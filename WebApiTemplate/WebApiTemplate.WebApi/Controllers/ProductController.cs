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
            //var vm = await _productService.CreateProductAsync(productInput);
            //return Ok(vm);
            return null;
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProductAsync(ProductInput productInput)
        {
            //var vm = await _productService.UpdateProductAsync(productInput);
            //return Ok(vm);
            return null;
        }

        [HttpDelete]
        [Route("delete-product")]
        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            //await _productService.DeleteProductAsync(productId);
            //return Ok();
            return null;
        }

    }
}