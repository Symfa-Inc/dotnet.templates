using WebApiTemplate.Application.Models.Product;

namespace WebApiTemplate.Application.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<ProductGetModelView>> GetProductsAsync();
        Task<ProductCreateModelView> CreateProductAsync(ProductCreateModel productCreateModel);
        Task<ProductUpdateModelView> UpdateProductAsync(int productId, ProductUpdateModel productUpdateModel);
        Task DeleteProductAsync(int productId);
    }
}
