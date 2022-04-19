using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Product.Interfaces;
using Serilog;
using WebApiTemplate.Application.Product.Models;

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
        public async Task<IActionResult> GetAsync()
        {
            var vm = await _productService.GetProductsAsync();
            Log.Information("REQUEST. ProductsNumber: {@ProductsNumber}", vm.Count);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductCreateModel productCreateModel)
        {
            var vm = await _productService.CreateProductAsync(productCreateModel);
            return Ok(vm);
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int productId, ProductUpdateModel productUpdateModel)
        {
            var vm = await _productService.UpdateProductAsync(productId, productUpdateModel);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return Ok();
        }

    }
}