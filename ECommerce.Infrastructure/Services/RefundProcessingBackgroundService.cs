using AutoMapper;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace ECommerce.Infrastructure.Services;

public class RefundProcessingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

    public RefundProcessingBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var refundService = scope.ServiceProvider.GetRequiredService<IRefundService>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            try
            {
                var refunds = await unitOfWork.Refunds.GetAllAsync();

                foreach (var refund in refunds)
                {
                    var gatewayResponse = await refundService.ProcessRefundPaymentAsync(refund);

                    if (gatewayResponse.IsSuccess)
                    {
                        refund.Status = RefundStatus.Completed;
                        refund.TransactionId = gatewayResponse.TransactionId;
                        refund.CompletedAt = DateTime.UtcNow;
                        refund.Payment.Status = PaymentStatus.Refunded;

                        unitOfWork.Payments.Update(refund.Payment);
                        unitOfWork.Refunds.Update(refund);
                        await unitOfWork.SaveChangesAsync();

                        if (refund.Cancellation?.Order?.Customer != null &&
                            !string.IsNullOrEmpty(refund.Cancellation.Order.Customer.Email))
                        {
                            string body = emailTemplateService.GetRefundSuccessTemplate(
                                refund,
                                refund.Cancellation.Order.OrderNumber,
                                refund.Cancellation
                            );
                            string subject = $"Your Refund Has Been Processed Successfully, Order #{refund.Cancellation.Order.OrderNumber}";
                            await emailService.SendEmailAsync(refund.Cancellation.Order.Customer.Email, subject, body, true);
                        }
                    }
                    else
                    {
                        refund.Status = gatewayResponse.Status;
                        refund.CompletedAt = DateTime.UtcNow;
                        unitOfWork.Refunds.Update(refund);
                        await unitOfWork.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await Task.Delay(_checkInterval, stoppingToken);
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }
}
