using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Product.Models
{
    public class ProductUpdateModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class ProductUpdateModelViewExtension
    {
        public static ProductUpdateModelView ToProductUpdateView(this Entities.Product product)
        {
            return new ProductUpdateModelView
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
