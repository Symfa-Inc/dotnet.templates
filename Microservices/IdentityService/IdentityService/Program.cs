using IdentityService.Data;
using IdentityService.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.ConfigurePipeline();
if (args.Contains("fill-tables"))
{
    DataSeed.FillTables(app);
}

app.Run();
