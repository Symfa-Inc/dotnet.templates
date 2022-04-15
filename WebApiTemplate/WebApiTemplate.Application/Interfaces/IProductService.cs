using WebApiTemplate.Application.Models.Product;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductView>> GetProductsAsync();
    }
}
