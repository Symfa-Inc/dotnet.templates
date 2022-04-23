using AuthorizationServer;
using AuthorizationServer.Data;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Setting up DB contexts
const string connectionStringName = "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionStringName);
builder.Services.AddDbContext<UserDbContext>(
    options =>
    {
        // Configure the context to use a store.
        options.UseSqlServer(connectionString);

        // Register the entity sets needed by OpenIddict.
        options.UseOpenIddict();
    });

// Adding Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
        options =>
        {
            // Password requirements
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireNonAlphanumeric = false;

            // Use the OpenIddict claim types
            options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
            options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
        })
    .AddEntityFrameworkStores<UserDbContext>();

// Adding OpenIddict
builder.Services.AddOpenIddict()

    // Register the OpenIddict core components.
    .AddCore(
        options =>
        {
            // Configure OpenIddict to use the EF Core stores/models.
            options.UseEntityFrameworkCore()
                .UseDbContext<UserDbContext>();
        })

    // Register the OpenIddict server components.
    .AddServer(
        options =>
        {
            options.AllowPasswordFlow()
                .AllowRefreshTokenFlow();

            // Setting up URIs
            options.SetTokenEndpointUris("/connect/token");

            // Encryption and signing of tokens
            // On production, using a X.509 certificate stored in the machine store is recommended.
            // https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios
            options.AddEphemeralEncryptionKey()
                .AddEphemeralSigningKey();

            options.SetAccessTokenLifetime(TimeSpan.FromDays(1));
            options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));

            // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
            options.UseAspNetCore()
                .EnableTokenEndpointPassthrough();
        });

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