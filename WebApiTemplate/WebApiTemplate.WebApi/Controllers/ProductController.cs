using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Interfaces;
using Serilog;
using Newtonsoft.Json;
using WebApiTemplate.Application.Models.Product;

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

        [HttpPost]
        [Route("create-product")]
        public async Task<IActionResult> CreateProductAsync(ProductInput productInput)
        {
            var vm = await _productService.CreateProductAsync(productInput);
            return Ok(vm);
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProductAsync(int productId, ProductInput productInput)
        {
            var vm = await _productService.UpdateProductAsync(productId, productInput);
            return Ok(vm);
        }

        [HttpDelete]
        [Route("delete-product")]
        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            var vm = await _productService.DeleteProductAsync(productId);
            return Ok(vm);
        }

    }
}