using AuthorizationServer;
using AuthorizationServer.Extensions;
using AuthorizationServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            policy => policy.WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddOpeniddict(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddCustomDependencies();
builder.Services.AddExternalProviders(builder.Configuration);

// Register the service to seeding the data to DB
builder.Services.AddHostedService<SeedDataService>();

var app = builder.Build();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();