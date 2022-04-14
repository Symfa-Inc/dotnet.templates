using WebApiTemplate.Domain.Dto;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
