using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Application.Product.Models
{
    public class ProductCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
