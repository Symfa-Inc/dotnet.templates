using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Product.Models
{
    public class ProductCreateModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class ProductCreateModelViewExtension
    {
        public static ProductCreateModelView ToProductCreateView(this Entities.Product product)
        {
            return new ProductCreateModelView
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
