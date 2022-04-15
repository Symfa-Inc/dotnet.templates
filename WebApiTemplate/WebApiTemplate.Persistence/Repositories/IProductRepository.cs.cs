using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Domain.Dto;

namespace WebApiTemplate.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
