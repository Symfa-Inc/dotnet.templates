namespace WebApiTemplate.Application.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string name, string subject, string body);
    }
}
