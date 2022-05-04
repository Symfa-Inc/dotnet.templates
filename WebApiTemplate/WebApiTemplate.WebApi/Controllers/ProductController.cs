using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Product.Interfaces;
using WebApiTemplate.Application.Product.Models;
using System.ComponentModel.DataAnnotations;

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
            var vm = await _productService.Get();
            _logger.LogInformation("REQUEST. ProductsNumber: {@ProductsNumber}", vm.Count);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required] ProductCreateModel productCreateModel)
        {
            var vm = await _productService.Create(productCreateModel);
            return Ok(vm);
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [Required] ProductUpdateModel productUpdateModel)
        {
            var vm = await _productService.Update(productId, productUpdateModel);
            return Ok(vm);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.Delete(productId);
            return Ok();
        }

    }
}