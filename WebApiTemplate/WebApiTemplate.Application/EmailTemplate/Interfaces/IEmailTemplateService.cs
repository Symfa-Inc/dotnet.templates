using WebApiTemplate.Domain.Enums.EmailTemplate;
using WebApiTemplate.Application.Email.Interfaces;

namespace WebApiTemplate.Application.EmailTemplate.Interfaces
{
    public interface IEmailTemplateService
    {
        Task SendEmailTemplateAsync(string email, EmailTemplateType type, Dictionary<string, string> paramDict = null);
    }
}
