using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Application.Product.Models
{
    public class ProductUpdateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
