namespace WebApiTemplate.Application.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string body, bool isBodyHtml);
    }
}
