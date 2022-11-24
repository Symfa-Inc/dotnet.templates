using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Persistence.EntityTypeConfigurations
{
    public class UserProfileDbConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId).IsUnique();
        }
    }
}
