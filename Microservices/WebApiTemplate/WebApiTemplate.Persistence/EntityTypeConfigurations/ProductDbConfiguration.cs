using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Persistence.EntityTypeConfigurations
{
    public class ProductDbConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
