using AuthorizationServer.Data;
using AuthorizationServer.Handlers;
using AuthorizationServer.Handlers.Interfaces;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using AuthorizationServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace AuthorizationServer.Extensions;

public static class StartupExtensions
{
    /// <summary>
    /// Add the database context
    /// </summary>
    public static void AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<UserDbContext>(
            options =>
            {
                // Configure the context to use a store.
                options.UseSqlServer(connectionString);

                // Register the entity sets needed by OpenIddict.
                options.UseOpenIddict();
            });
    }

    /// <summary>
    /// Add the identity system
    /// </summary>
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    // Password requirements
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = false;

                    // Use the OpenIddict claim types
                    options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                    options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                    options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                    options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
                })
            .AddEntityFrameworkStores<UserDbContext>();
    }

    /// <summary>
    /// Register the OpenIddict components
    /// </summary>
    public static void AddOpeniddict(this IServiceCollection services)
    {
        services.AddOpenIddict()

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
                    options.SetTokenEndpointUris("/connect/token")
                        // Allows the resource server using token validation by sending a request to this server instead of local validation.
                        // This feature can be turn on on the resource server side:
                        // services.AddOpenIddict()
                        //    .AddValidation(options =>
                        //    {
                        //        options.UseIntrospection();
                        //    });
                        .SetIntrospectionEndpointUris("/connect/introspection")
                        .SetRevocationEndpointUris("/connect/revoke");

                    // Encryption and signing of tokens
                    // On production, using a X.509 certificate stored in the machine store is recommended.
                    // https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios
                    options.AddEphemeralSigningKey()
                        .AddEphemeralEncryptionKey();

                    options.SetAccessTokenLifetime(TimeSpan.FromDays(1));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));

                    // Make refresh token invalid after refresh
                    options.SetRefreshTokenReuseLeeway(TimeSpan.Zero);

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough();
                });
    }

    /// <summary>
    /// Register custom dependencies
    /// </summary>
    public static void AddCustomDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPasswordGrantTypeHandler, PasswordGrantTypeHandler>();
        services.AddScoped<IRefreshTokenGrantTypeHandler, RefreshTokenGrantTypeHandler>();
        services.AddScoped<ITokenIssueService, TokenIssueService>();
        services.AddScoped<IUserService, UserService>();
    }
}