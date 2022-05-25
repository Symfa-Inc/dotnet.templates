using WebApiTemplate.Application.Email.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.Application.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string email, string subject, string body, bool isBodyHtml)
        {
            if (!IsEnabled())
            {
                throw new CommonException(ErrorCode.EmailServiceDisabled);
            }

            var websiteEmailName = _configuration["MailSettings:Name"];
            var websiteEmail = _configuration["MailSettings:Email"];
            var websiteEmailPassword = _configuration["MailSettings:Password"];
            var smtpHost = _configuration["MailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["MailSettings:SmtpPort"]);

            var fromAddress = new MailAddress(websiteEmail, websiteEmailName);
            var toAddress = new MailAddress(email);

            var smtp = new SmtpClient
            {
                Host = smtpHost,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, websiteEmailPassword)
            };

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
            };

            await smtp.SendMailAsync(message);
        }

        private bool IsEnabled()
        {
            var isEnabled = _configuration["MailSettings:IsEnabled"];

            if (!bool.TryParse(isEnabled, out bool value))
            {
                throw new CommonException(ErrorCode.EmailServiceInvalidConfig);
            }

            return value;
        }
    }
}
