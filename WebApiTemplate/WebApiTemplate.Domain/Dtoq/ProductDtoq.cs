using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Domain.Dto
{
    public class ProductDtoq
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class ProductDtoExtension
    {
        public static ProductDtoq ToDto(this Product product)
        {
            return new ProductDtoq
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
    }
}
