using WebApiTemplate.Domain.DTO;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
