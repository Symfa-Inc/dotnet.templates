using WebApiTemplate.Persistence;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Services;
using WebApiTemplate.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

const string BaseDirectory = "[BaseDirectory]";

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

configuration.SetBasePath(environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHsts(options => options.MaxAge = TimeSpan.FromDays(365));
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("ConnectionString");

if (connectionString.Contains(BaseDirectory))
{
    string contentRootPath = Directory.GetCurrentDirectory();
    connectionString = connectionString.Replace(BaseDirectory, contentRootPath);
}

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddScoped<IProductService, ProductService>();

builder.Services
    .AddScoped<IProductRepository, ProductRepository>();

builder.Host.UseSerilog((context, config) =>
{
    string logPath = builder.Configuration["Settings:LogPath"];
    string logName = builder.Configuration["Settings:LogName"];
    string logPathFull = Path.Combine(logPath, logName);

    config.WriteTo.File(logPathFull, rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    DatabaseInitializer.Initialize(databaseContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    });
    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
