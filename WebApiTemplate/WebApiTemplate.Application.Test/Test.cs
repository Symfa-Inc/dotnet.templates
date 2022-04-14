using Xunit;
using WebApiTemplate.Domain.DTO;
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
        const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=[BaseDirectory]\\App_Data\\WebApiTemplate.mdf;Initial Catalog=WebApiTemplate;Trusted_Connection=True;";
        const string BaseDirectory = "[BaseDirectory]";
        const string BaseDirectoryPath = "C:\\Kovaluk\\projects\\ais.dotnet.templates\\WebApiTemplate\\WebApiTemplate.WebApi";

        [Fact]
        public async void TestProducts()
        {
            string connectionString = GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                IProductService productService = new ProductService(context);
                IEnumerable<ProductDto> products = await productService.GetProductsAsync();
                Assert.True(products != null && products.Any());
            }
        }

        #region PrivateMethods

        private string GetConnectionString()
        {
            var connectionString = ConnectionString;

            if (connectionString.Contains(BaseDirectory))
            {
                connectionString = connectionString.Replace(BaseDirectory, BaseDirectoryPath);
            }

            return connectionString;
        }

        #endregion
    }
}