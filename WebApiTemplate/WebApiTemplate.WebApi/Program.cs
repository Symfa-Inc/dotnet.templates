using WebApiTemplate.Persistence;
using WebApiTemplate.Application.Product.Interfaces;
using WebApiTemplate.Application.Product.Services;
using WebApiTemplate.Application.EmailTemplate.Interfaces;
using WebApiTemplate.Application.EmailTemplate.Services;
using WebApiTemplate.Application.Email.Interfaces;
using WebApiTemplate.Application.Email.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

const string BaseDirectory = "[BaseDirectory]";

WebApplicationBuilder _builder;
ConfigurationManager _configuration;
IWebHostEnvironment _environment;
WebApplication _app;
string _connectionString;

InitBuilder();
AddConfig();
InitServices();
InitConnectionString();
AddServices();
AddLogging();
BuildWebApplication();
InitDatabaseMigrations();
ConfigureWebApplication();

void InitBuilder()
{
    _builder = WebApplication.CreateBuilder(args);
    _configuration = _builder.Configuration;
    _environment = _builder.Environment;
}

void AddConfig()
{
    _configuration.SetBasePath(_environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();
}

void InitServices()
{
    _builder.Services.AddControllers();
    _builder.Services.AddHsts(options => options.MaxAge = TimeSpan.FromDays(365));
    _builder.Services.AddCors();
    _builder.Services.AddEndpointsApiExplorer();
    _builder.Services.AddSwaggerGen();
}

void InitConnectionString()
{
    _connectionString = _builder.Configuration.GetConnectionString("ConnectionString");
    if (_connectionString.Contains(BaseDirectory))
    {
        string contentRootPath = Directory.GetCurrentDirectory();
        _connectionString = _connectionString.Replace(BaseDirectory, contentRootPath);
    }
}

void AddServices()
{
    _builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(_connectionString));
    _builder.Services.AddScoped<IProductService, ProductService>();
    _builder.Services.AddScoped<IEmailService, EmailService>();
    _builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
}

void AddLogging()
{
    string logPath = _builder.Configuration["Settings:LogPath"];
    string logName = _builder.Configuration["Settings:LogName"];
    string logPathFull = Path.Combine(logPath, logName);

    _builder.Host.UseSerilog((context, config) =>
    {
        config.WriteTo.File(logPathFull, rollingInterval: RollingInterval.Day);
    });
}

void BuildWebApplication()
{
    _app = _builder.Build();
}

void InitDatabaseMigrations()
{
    using (var scope = _app.Services.CreateScope())
    {
        DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        DatabaseInitializer.Initialize(databaseContext);
    }
}

void ConfigureWebApplication()
{
    if (_app.Environment.IsDevelopment())
    {
        _app.UseDeveloperExceptionPage();

        _app.UseSwagger();
        _app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        });
        _app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }
    else
    {
        _app.UseHsts();
    }

    _app.UseHttpsRedirection();
    _app.UseAuthorization();
    _app.MapControllers();
    _app.Run();
}