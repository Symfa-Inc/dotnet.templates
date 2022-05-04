using WebApiTemplate.Application.EmailTemplate.Interfaces;
using WebApiTemplate.Domain.Enums.EmailTemplate;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Persistence;
using WebApiTemplate.Domain.Errors.EmailTemplate;
using WebApiTemplate.Application.Email.Interfaces;
using System.Text;

namespace WebApiTemplate.Application.EmailTemplate.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly DatabaseContext _context;
        private readonly IEmailService _emailService;

        public EmailTemplateService(DatabaseContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task SendEmailTemplate(string email, EmailTemplateType type, Dictionary<string, string> paramDict = null)
        {
            var emailTemplate = await _context.EmailTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type == type);

            if (emailTemplate == null)
            {
                throw new EmailTemplateNotFoundException();
            }

            var stringBuilderEmailBody = new StringBuilder(emailTemplate.Body);

            if (paramDict != null)
            {
                foreach (var keyValue in paramDict)
                {
                    stringBuilderEmailBody.Replace(keyValue.Key, keyValue.Value);
                }
            }

            await _emailService.SendEmail(email, emailTemplate.Subject, stringBuilderEmailBody.ToString(), true);
        }
    }
}
