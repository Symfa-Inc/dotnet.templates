using WebApiTemplate.Application.Product.Models;

namespace WebApiTemplate.Application.Product.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<ProductGetModelView>> Get();
        Task<ProductCreateModelView> Create(ProductCreateModel productCreateModel);
        Task<ProductUpdateModelView> Update(int productId, ProductUpdateModel productUpdateModel);
        Task Delete(int productId);
    }
}
