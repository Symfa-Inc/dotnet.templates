using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Domain.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class ProductDtoExtension
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
