using Xunit;
using System.Collections.Generic;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Services;
using System.Linq;
using WebApiTemplate.Persistence;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Persistence.Repositories;
using WebApiTemplate.Application.Models.Product;
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
                IProductRepository productRepository = new ProductRepository(context);
                IProductService productService = new ProductService(productRepository);
                IEnumerable<ProductView> products = await productService.GetProductsAsync();
                Assert.True(products != null && products.Any());
            }
        }
    }
}