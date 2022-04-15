using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Persistence.Repositories;
using WebApiTemplate.Application.Models.Product;

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
    }
}
