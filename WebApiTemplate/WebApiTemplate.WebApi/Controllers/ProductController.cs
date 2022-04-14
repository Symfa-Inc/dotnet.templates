using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Interfaces;

namespace WebApiTemplate.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get-products")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var vm = await _productService.GetProductsAsync();
            return Ok(vm);
        }
    }
}