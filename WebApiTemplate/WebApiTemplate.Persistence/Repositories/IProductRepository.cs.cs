using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Domain.Dto;

namespace WebApiTemplate.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> CreateProductAsync(Product product);
        Task<ProductDto> UpdateProductAsync(int productId, Product product);
        Task<bool> DeleteProductAsync(int productId);
    }
}
