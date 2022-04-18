using IdentityService.Data;
using IdentityService.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.ConfigurePipeline();
if (args.Contains("init-database"))
{
    DataSeed.InitializeDatabase(app);
}

app.Run();
