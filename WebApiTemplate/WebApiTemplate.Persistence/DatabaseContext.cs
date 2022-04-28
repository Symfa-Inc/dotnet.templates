using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Persistence.Interfaces;
using WebApiTemplate.Persistence.EntityTypeConfigurations;

namespace WebApiTemplate.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ProductDbConfiguration).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
