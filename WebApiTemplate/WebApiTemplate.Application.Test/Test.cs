using Xunit;
using WebApiTemplate.Domain.Dto;
using System.Collections.Generic;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Services;
using System.Linq;
using WebApiTemplate.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebApiTemplate.Application.Integration.Test
{
    public class Test
    {
        const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Kovaluk\\projects\\ais.dotnet.templates\\WebApiTemplate\\WebApiTemplate.WebApi\\App_Data\\WebApiTemplate.mdf;Initial Catalog=WebApiTemplate;Trusted_Connection=True;";

        [Fact]
        public async void TestProducts()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optionsBuilder.UseSqlServer(ConnectionString);

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                IProductService productService = new ProductService(context);
                IEnumerable<ProductDto> products = await productService.GetProductsAsync();
                Assert.True(products != null && products.Any());
            }
        }
    }
}