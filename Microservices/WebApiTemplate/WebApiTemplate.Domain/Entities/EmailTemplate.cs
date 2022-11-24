using WebApiTemplate.Domain.Enums.EmailTemplate;

namespace WebApiTemplate.Domain.Entities
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmailTemplateType Type { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
