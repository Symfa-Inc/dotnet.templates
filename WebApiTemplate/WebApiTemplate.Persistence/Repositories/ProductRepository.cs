using WebApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Domain.Dto;

namespace WebApiTemplate.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDtoq>> GetProductsAsync()
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return products.Select(x => x.ToDto());
        }

        public async Task CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
