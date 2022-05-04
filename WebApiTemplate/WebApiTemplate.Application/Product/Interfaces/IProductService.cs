using WebApiTemplate.Application.Product.Models;

namespace WebApiTemplate.Application.Product.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<ProductGetModelView>> GetProducts();
        Task<ProductCreateModelView> CreateProduct(ProductCreateModel productCreateModel);
        Task<ProductUpdateModelView> UpdateProduct(int productId, ProductUpdateModel productUpdateModel);
        Task DeleteProduct(int productId);
    }
}
