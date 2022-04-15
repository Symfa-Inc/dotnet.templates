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

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return products.Select(x => x.ToDto());
        }

        public async Task<ProductDto> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ToDto();
        }

        public async Task<ProductDto> UpdateProductAsync(int productId, Product product)
        {
            var productBase = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (productBase == null) return null;
            productBase.Name = product.Name;
            await _context.SaveChangesAsync();
            return productBase.ToDto();
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
