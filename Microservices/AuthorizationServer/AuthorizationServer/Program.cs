using AuthorizationServer;
using AuthorizationServer.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

const string connectionStringName = "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionStringName);
builder.Services.AddDbContext(connectionString);
builder.Services.AddIdentity();
builder.Services.AddOpeniddict();
builder.Services.AddCustomDependencies();

// Register the service to seeding the data to DB
builder.Services.AddHostedService<SeedDataService>();

var app = builder.Build();
app.UseRouting();
app.UseCors(
    policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();