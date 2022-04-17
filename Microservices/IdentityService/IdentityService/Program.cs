using IdentityService.Data;
using IdentityService.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.ConfigurePipeline();
if (args[0] == "init-database")
{
    DataSeed.InitializeDatabase(app);
}

app.Run();
