using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Domain.DTO;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Persistence;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly DatabaseContext _context;

        public ProductService(
            DatabaseContext context
            )
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            List<Product> products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return products.Select(x => x.ToDTO());
        }
    }
}
