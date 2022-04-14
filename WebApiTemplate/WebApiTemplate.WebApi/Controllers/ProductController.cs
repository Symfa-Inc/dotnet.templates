using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Interfaces;
using Serilog;
using Newtonsoft.Json;

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
            Log.Information($@"REQUEST. 
                               Controller: '{nameof(ProductController)}'. 
                               Endpoint: '{nameof(GetProductsAsync)}'.
                               Response: '{JsonConvert.SerializeObject(vm)}'");
            return Ok(vm);
        }

        // TODO: add post, put, delete
    }
}