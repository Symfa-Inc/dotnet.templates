using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Persistence.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<Product> Products { get; set; }
    }
}
