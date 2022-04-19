using Xunit;
using System.Collections.Generic;
using WebApiTemplate.Application.Product.Interfaces;
using WebApiTemplate.Application.Product.Services;
using System.Linq;
using WebApiTemplate.Persistence;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Application.Product.Models;
using Microsoft.Extensions.Configuration;

namespace WebApiTemplate.Application.Integration.Test
{
    public class ApplicationIntegrationTest
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationIntegrationTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();

            _connectionString = _configuration.GetConnectionString("ConnectionString");
        }

        [Fact]
        public async void TestProductsAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optionsBuilder.UseSqlServer(_connectionString);

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                IProductService productService = new ProductService(context);
                var products = await productService.GetProductsAsync();
                Assert.True(products != null && products.Any());
            }
        }
    }
}