using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
    {

        string? MailServer = _configuration["EmailSettings:MailServer"];

        string? FromEmail = _configuration["EmailSettings:FromEmail"];

        string? Password = _configuration["EmailSettings:Password"];

        string? SenderName = _configuration["EmailSettings:SenderName"];

        int Port = Convert.ToInt32(_configuration["EmailSettings:MailPort"]);

        var client = new SmtpClient(MailServer, Port)
        {
            Credentials = new NetworkCredential(FromEmail, Password),

            EnableSsl = true,
        };

        MailAddress fromAddress = new MailAddress(FromEmail, SenderName);

        MailMessage mailMessage = new MailMessage
        {
            From = fromAddress, 
            Subject = Subject, 
            Body = Body,
            IsBodyHtml = IsBodyHtml 
        };

        mailMessage.To.Add(ToEmail);

        return client.SendMailAsync(mailMessage);
    }

    public async Task SendCancellationAcceptedEmailAsync(Cancellation cancellation)
    {
        if (cancellation.Order == null || cancellation.Order.Customer == null)
            return;

        string subject = $"Cancellation Request Update - Order #{cancellation.Order.OrderNumber}";
        string body = _emailTemplateService.GetCancellationAcceptedTemplate(cancellation);

        await SendEmailAsync(cancellation.Order.Customer.Email, subject, body, true);
    }

    public async Task SendCancellationRejectedEmailAsync(Cancellation cancellation)
    {
        if (cancellation.Order == null || cancellation.Order.Customer == null)
            return;

        string subject = $"Cancellation Request Rejected - Order #{cancellation.Order.OrderNumber}";
        string body = _emailTemplateService.GetCancellationRejectedTemplate(cancellation);

        await SendEmailAsync(cancellation.Order.Customer.Email, subject, body, true);
    }

    public async Task SendRefundSuccessEmailAsync(Refund refund, Cancellation cancellation, string? customSubject = null)
    {
        if (cancellation.Order?.Customer?.Email == null)
            return;

        string subject = $"Your Refund Has Been Processed Successfully, Order #{cancellation.Order.OrderNumber}";
        string body = _emailTemplateService.GetRefundSuccessTemplate(refund, cancellation.Order.OrderNumber, cancellation);

        await SendEmailAsync(cancellation.Order.Customer.Email, subject, body, true);
    }

}
