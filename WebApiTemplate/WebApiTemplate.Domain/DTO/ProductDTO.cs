using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public static class ProductDTOExtension
    {
        public static ProductDTO ToDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
