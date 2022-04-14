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

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductDbConfiguration());

            base.OnModelCreating(builder);
        }
        // TODO add repositories
    }
}
