using WebApiTemplate.Domain.Dto;

namespace WebApiTemplate.Application.Models.Product
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class ProductViewExtension
    {
        public static ProductView ToView(this ProductDtoq productDto)
        {
            return new ProductView
            {
                Id = productDto.Id,
                Name = productDto.Name,
            };
        }
    }
}
