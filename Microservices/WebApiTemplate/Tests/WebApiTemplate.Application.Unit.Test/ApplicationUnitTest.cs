using Xunit;
using WebApiTemplate.Application.Product.Interfaces;
using WebApiTemplate.Application.Product.Services;
using System.Linq;
using WebApiTemplate.Persistence;
using Microsoft.EntityFrameworkCore;
using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Unit.Test
{
    public class ApplicationUnitTest
    {
        [Fact]
        public async void TestProducts()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "WebApiTemplate")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                context.Products.Add(new Entities.Product { Id = 1, Name = "Ball" });
                context.Products.Add(new Entities.Product { Id = 2, Name = "Table" });
                context.Products.Add(new Entities.Product { Id = 3, Name = "Chair" });
                await context.SaveChangesAsync();

                IProductService productService = new ProductService(context, null, null);
                var products = await productService.Get();
                Assert.True(products != null && products.Any());
            }
        }
    }
}