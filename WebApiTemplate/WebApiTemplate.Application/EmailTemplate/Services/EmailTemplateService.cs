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

        public async Task SendEmailTemplateAsync(string email, string name, EmailTemplateType type)
        {
            var emailTemplate = await _context.EmailTemplates.FirstOrDefaultAsync(x => x.Type == type);

            if (emailTemplate == null)
            {
                throw new EmailTemplateNotFoundException();
            }

            await _emailService.SendEmail(email, name, emailTemplate.Subject, emailTemplate.Body);
        }
    }
}
