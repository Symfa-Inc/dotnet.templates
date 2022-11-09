using AuthorizationServer.Data;
using AuthorizationServer.Handlers.Actions;
using AuthorizationServer.Handlers.GrantTypes;
using AuthorizationServer.Interfaces.Handlers.Actions;
using AuthorizationServer.Interfaces.Handlers.GrantTypes;
using AuthorizationServer.Interfaces.Services;
using AuthorizationServer.Middlewares;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace AuthorizationServer.Extensions;

public static class StartupExtensions
{
    /// <summary>
    /// Add the database context
    /// </summary>
    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<UserDbContext>(
            options =>
            {
                // Configure the context to use a store.
                options.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));

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
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

        services.AddDistributedMemoryCache();
        services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(5));
    }

    /// <summary>
    /// Register the OpenIddict components
    /// </summary>
    public static void AddOpeniddict(this IServiceCollection services, ConfigurationManager configurationManager, bool isDevelopment)
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
                    options.Configure(
                        serverOptions =>
                        {
                            serverOptions.TokenValidationParameters.ValidIssuers = new List<string>
                                {
                                    "http://localhost:5001/"
                                };
                        });

                    options.AllowAuthorizationCodeFlow()
                        .RequireProofKeyForCodeExchange()
                        .AllowPasswordFlow()
                        .AllowRefreshTokenFlow()
                        // Add a custom code flow to check a verification code entered by user after a credential verification
                        .AllowCustomFlow("2fa_code");

                    options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Roles);

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
                        .SetRevocationEndpointUris("/connect/revoke")
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetUserinfoEndpointUris("/account/userinfo");

                    // Encryption and signing of tokens
                    // On production, you can using a X.509 certificate stored in the machine store is recommended.
                    // https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios
                    if (isDevelopment)
                    {
                        options.AddEphemeralSigningKey()
                            .AddEphemeralEncryptionKey();
                    }
                    else
                    {
                        options.AddSigningCertificate(
                                new FileStream(
                                    configurationManager["Token:SigningCertificate:Path"],
                                    FileMode.Open),
                                configurationManager["Token:SigningCertificate:Password"])
                            .AddEncryptionCertificate(
                                new FileStream(
                                    configurationManager["Token:EncryptionCertificate:Path"],
                                    FileMode.Open),
                                configurationManager["Token:EncryptionCertificate:Password"]);
                    }

                    options.SetAccessTokenLifetime(
                        TimeSpan.FromSeconds(int.Parse(configurationManager["Token:ExpirationTime:AccessToken"])));
                    options.SetRefreshTokenLifetime(
                        TimeSpan.FromSeconds(int.Parse(configurationManager["Token:ExpirationTime:AccessToken"])));

                    // Make refresh token invalid after refresh
                    options.SetRefreshTokenReuseLeeway(TimeSpan.Zero);

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options.UseAspNetCore()
                        .EnableAuthorizationEndpointPassthrough()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserinfoEndpointPassthrough();
                })

            // Validation is necessary because some endpoints have to be protected
            .AddValidation(
                options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
    }

    /// <summary>
    /// Register custom dependencies
    /// </summary>
    public static void AddCustomDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPasswordGrantTypeHandler, PasswordGrantTypeHandler>();
        services.AddScoped<IRefreshTokenGrantTypeHandler, RefreshTokenGrantTypeHandler>();
        services.AddScoped<IAuthorizationCodeGrantTypeHandler, AuthorizationCodeGrantTypeHandler>();
        services.AddScoped<ITwoFactorAuthenticationGrantTypeHandler, TwoFactorAuthenticationGrantTypeHandler>();
        services.AddScoped<ITokenIssueHandler, TokenIssueHandler>();
        services.AddScoped<IExternalProviderHandler, ExternalProviderHandler>();
        services.AddScoped<IUserCreatorService, UserCreatorService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUserEmailSender, UserEmailSender>();
        services.AddTransient<ExceptionHandlerMiddleware>();
    }

    public static void AddExternalProviders(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddAuthentication()
            .AddGoogle(options => configurationManager.GetSection("Google").Bind(options))
            .AddFacebook(options => configurationManager.GetSection("Facebook").Bind(options))
            .AddTwitter(options => configurationManager.GetSection("Twitter").Bind(options));
    }
}