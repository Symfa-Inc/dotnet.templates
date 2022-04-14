using WebApiTemplate.Application.Models.DTO;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
    }
}
