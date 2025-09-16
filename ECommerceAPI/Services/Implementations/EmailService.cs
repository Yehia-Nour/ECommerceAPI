using ECommerceAPI.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ECommerceAPI.Services.Implementations
{
    public class EmailService : IEmailService
    {
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
    }
}
