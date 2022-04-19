using Xunit;
using System.Collections.Generic;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Services;
using System.Linq;
using WebApiTemplate.Persistence;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.Models.Product;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.Unit.Test
{
    public class ApplicationUnitTest
    {
        [Fact]
        public async void TestProductsAsync()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "WebApiTemplate")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "Ball" });
                context.Products.Add(new Product { Id = 2, Name = "Table" });
                context.Products.Add(new Product { Id = 3, Name = "Chair" });
                context.SaveChanges();

                IProductService productService = new ProductService(context);
                var products = await productService.GetProductsAsync();
                Assert.True(products != null && products.Any());
            }
        }
    }
}