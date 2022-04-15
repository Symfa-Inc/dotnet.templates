using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Persistence.Repositories;
using WebApiTemplate.Application.Models.Product;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductView>> GetProductsAsync()
        {
            var productsDto = await _productRepository.GetProductsAsync();
            return productsDto.Select(x => x.ToView());
        }

        public async Task<ProductView> CreateProductAsync(ProductInput productInput)
        {
            var product = new Product { Name = productInput.Name };
            var productNew = await _productRepository.CreateProductAsync(product);
            return productNew.ToView();
        }

        public async Task<ProductView> UpdateProductAsync(int productId, ProductInput productInput)
        {
            var product = new Product { Name = productInput.Name };
            var productUpdated = await _productRepository.UpdateProductAsync(productId, product);
            return productUpdated.ToView();
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteProductAsync(productId);
        }
    }
}
