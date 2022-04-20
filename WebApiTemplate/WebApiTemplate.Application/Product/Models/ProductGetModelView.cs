using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Product.Models
{
    public class ProductGetModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class ProductGetModelViewExtension
    {
        public static ProductGetModelView ToProductGetView(this Entities.Product product)
        {
            return new ProductGetModelView
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
