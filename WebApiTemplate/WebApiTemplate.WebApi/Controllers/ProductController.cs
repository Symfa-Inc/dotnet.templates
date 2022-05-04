using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Product.Interfaces;
using WebApiTemplate.Application.Product.Models;

namespace WebApiTemplate.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vm = await _productService.GetProducts();
            _logger.LogInformation("REQUEST. ProductsNumber: {@ProductsNumber}", vm.Count);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel productCreateModel)
        {
            var vm = await _productService.CreateProduct(productCreateModel);
            return Ok(vm);
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, ProductUpdateModel productUpdateModel)
        {
            var vm = await _productService.UpdateProduct(productId, productUpdateModel);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.DeleteProduct(productId);
            return Ok();
        }

    }
}