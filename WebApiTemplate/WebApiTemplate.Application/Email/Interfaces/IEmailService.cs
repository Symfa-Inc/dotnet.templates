namespace WebApiTemplate.Application.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body, bool isBodyHtml);
    }
}
