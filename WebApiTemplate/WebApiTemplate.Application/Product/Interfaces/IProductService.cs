using WebApiTemplate.Application.Product.Models;

namespace WebApiTemplate.Application.Product.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<ProductGetModelView>> GetProductsAsync();
        Task<ProductCreateModelView> CreateProductAsync(ProductCreateModel productCreateModel);
        Task<ProductUpdateModelView> UpdateProductAsync(int productId, ProductUpdateModel productUpdateModel);
        Task DeleteProductAsync(int productId);
    }
}
