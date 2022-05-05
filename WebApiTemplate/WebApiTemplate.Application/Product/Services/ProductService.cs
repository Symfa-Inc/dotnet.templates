using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.Product.Interfaces;
using WebApiTemplate.Application.Product.Models;
using WebApiTemplate.Persistence;
using WebApiTemplate.Domain.Errors.Common;
using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<ProductGetModelView>> Get()
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return products.Select(x => x.ToProductGetView())
                .ToList()
                .AsReadOnly();
        }

        public async Task<ProductCreateModelView> Create(ProductCreateModel productCreateModel)
        {
            var product = new Entities.Product { Name = productCreateModel.Name };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ToProductCreateView();
        }

        public async Task<ProductUpdateModelView> Update(int productId, ProductUpdateModel productUpdateModel)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            product.Name = productUpdateModel.Name;
            await _context.SaveChangesAsync();
            return product.ToProductUpdateView();
        }

        public async Task Delete(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
