using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Persistence.EntityTypeConfigurations
{
    public class EmailTemplateDbConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
