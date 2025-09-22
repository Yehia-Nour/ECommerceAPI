using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml);
    Task SendCancellationAcceptedEmailAsync(Cancellation cancellation);
    Task SendCancellationRejectedEmailAsync(Cancellation cancellation);
    Task SendRefundSuccessEmailAsync(Refund refund, Cancellation cancellation, string? customSubject);
}
