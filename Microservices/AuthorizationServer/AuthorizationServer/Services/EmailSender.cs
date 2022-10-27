using System.Net;
using System.Net.Mail;
using AuthorizationServer.Interfaces.Services;

namespace AuthorizationServer.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var fromAddress = new MailAddress(_configuration["MailSettings:Email"]);
        var toAddress = new MailAddress(toEmail);
        using var smtpClient = new SmtpClient
        {
            Host = _configuration["MailSettings:SmtpHost"],
            Port = int.Parse(_configuration["MailSettings:SmtpPort"]),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(fromAddress.Address, _configuration["MailSettings:Password"])
        };

        await smtpClient.SendMailAsync(
            new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            });
    }
}