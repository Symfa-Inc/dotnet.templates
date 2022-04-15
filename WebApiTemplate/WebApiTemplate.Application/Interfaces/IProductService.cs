using WebApiTemplate.Application.Models.Product;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductView>> GetProductsAsync();
        Task<ProductView> CreateProductAsync(ProductInput productInput);
        Task<ProductView> UpdateProductAsync(int productId, ProductInput productInput);
        Task<bool> DeleteProductAsync(int productId);
    }
}
