using AutoMapper;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Interfaces.UnitOfWork;
using ECommerce.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Infrastructure.Services;

public class PendingPaymentService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

    public PendingPaymentService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            try
            {
                var pendingPayments = await unitOfWork.Payments.GetPendingPaymentsAsync();

                var ordersToEmail = new List<int>();

                foreach (var payment in pendingPayments)
                {
                    var updatedStatus = SimulatePaymentGatewayResponse();

                    if (updatedStatus == PaymentStatus.Completed)
                    {
                        payment.Status = PaymentStatus.Completed;
                        payment.TransactionId = Guid.NewGuid().ToString("N");
                        payment.Order.OrderStatus = OrderStatus.Processing;
                        ordersToEmail.Add(payment.Order.Id);
                    }
                    else if (updatedStatus == PaymentStatus.Failed)
                        payment.Status = PaymentStatus.Failed;

                    unitOfWork.Payments.Update(payment);
                }

                await unitOfWork.SaveChangesAsync();

                foreach (var orderId in ordersToEmail)
                {
                    var order = await unitOfWork.Orders.GetByIdAsync(orderId);
                    var payment = await unitOfWork.Payments.GetPaymentByOrderIdAsync(orderId);

                    if (order != null && payment != null)
                    {
                        string subject = $"Order Confirmation - {order.OrderNumber}";
                        string body = emailTemplateService.GetOrderConfirmationTemplate(order, payment);
                        await emailService.SendEmailAsync(order.Customer.Email, subject, body, true);
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

    private PaymentStatus SimulatePaymentGatewayResponse()
    {
        var rnd = new Random();
        int chance = rnd.Next(1, 101);

        if (chance <= 50)
            return PaymentStatus.Completed;
        else if (chance <= 80)
            return PaymentStatus.Failed;
        else
            return PaymentStatus.Pending;
    }
}
