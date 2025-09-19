using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        string GetOrderConfirmationTemplate(Order order, Payment? payment);
        string GetCancellationAcceptedTemplate(Cancellation cancellation);
        string GetCancellationRejectedTemplate(Cancellation cancellation);
    }
}
