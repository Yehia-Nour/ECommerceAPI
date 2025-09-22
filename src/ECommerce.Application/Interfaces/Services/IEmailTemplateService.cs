using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface IEmailTemplateService
{
    string GetOrderConfirmationTemplate(Order order, Payment? payment);
    string GetCancellationAcceptedTemplate(Cancellation cancellation);
    string GetCancellationRejectedTemplate(Cancellation cancellation);
    string GetRefundSuccessTemplate(Refund refund, string orderNumber, Cancellation cancellation);
}