using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Domain.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public static class ProductDTOExtension
    {
        public static ProductDto ToDTO(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
