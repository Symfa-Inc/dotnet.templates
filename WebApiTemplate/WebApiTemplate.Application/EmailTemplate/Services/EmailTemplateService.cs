using WebApiTemplate.Application.EmailTemplate.Interfaces;
using WebApiTemplate.Domain.Enums.EmailTemplate;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Persistence;
using WebApiTemplate.Domain.Errors.EmailTemplate;
using WebApiTemplate.Application.Email.Interfaces;

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

        public async Task SendEmailTemplateAsync(string email, EmailTemplateType type, Dictionary<string, string> paramDict = null)
        {
            var emailTemplate = await _context.EmailTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type == type);

            if (emailTemplate == null)
            {
                throw new EmailTemplateNotFoundException();
            }

            if (paramDict != null)
            {
                foreach (var keyValue in paramDict)
                {
                    emailTemplate.Body.Replace(keyValue.Key, keyValue.Value);
                }
            }

            await _emailService.SendEmail(email, emailTemplate.Subject, emailTemplate.Body);
        }
    }
}
