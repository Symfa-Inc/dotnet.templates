using AuthorizationServer.Constants;
using AuthorizationServer.Data;
using OpenIddict.Abstractions;
using Microsoft.AspNetCore.Identity;
using AuthorizationServer.Models;

namespace AuthorizationServer
{
    public class SeedDataService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedDataService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            // Create database if needed
            var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            await userDbContext.Database.EnsureCreatedAsync(cancellationToken);

            // Register new clients
            var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            await RegisterClientsAsync(applicationManager, cancellationToken);

            // Create roles
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roleNames = new[] { UserRoles.AuthorizationAdmin, UserRoles.ApplicationAdmin, UserRoles.User };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create AuthorizationAdmin
            await CreateAdminAsync(userManager, AuthorizationAdminUser.UserName, AuthorizationAdminUser.Email, AuthorizationAdminUser.Password, UserRoles.AuthorizationAdmin);

            // Create ApplicationAdmin
            await CreateAdminAsync(userManager, ApplicationAdminUser.UserName, ApplicationAdminUser.Email, ApplicationAdminUser.Password, UserRoles.ApplicationAdmin);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private static async Task RegisterClientsAsync(
            IOpenIddictApplicationManager applicationManager,
            CancellationToken cancellationToken)
        {
            // You can register your own client(s). SPA app, for instance
            await AddSpaClientAsync(applicationManager, cancellationToken);
            await AddBackendClientAsync(applicationManager, cancellationToken);
        }

        private static async Task AddSpaClientAsync(IOpenIddictApplicationManager applicationManager, CancellationToken cancellationToken)
        {
            if (await applicationManager.FindByClientIdAsync(ClientName.SpaClient, cancellationToken) == null)
            {
                await applicationManager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = ClientName.SpaClient,

                        // Specify URLs application can redirect to
                        RedirectUris =
                        {
                            new Uri("http://localhost:3000/authCallback")
                        },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.Endpoints.Revocation,
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.ResponseTypes.Code,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            OpenIddictConstants.Permissions.Prefixes.GrantType + CustomGrantType.TwoFactorAuthentication,
                            OpenIddictConstants.Permissions.Scopes.Email
                        }
                    },
                    cancellationToken);
            }
        }

        private static async Task AddBackendClientAsync(
            IOpenIddictApplicationManager applicationManager,
            CancellationToken cancellationToken)
        {
            if (await applicationManager.FindByClientIdAsync(ClientName.BackendClient, cancellationToken) == null)
            {
                await applicationManager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = ClientName.BackendClient,
                        ClientSecret = "secret",
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Introspection
                        }
                    },
                    cancellationToken);
            }
        }

        private async Task CreateAdminAsync(UserManager<ApplicationUser> userManager, string userName, string email, string password, string role)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                return;
            }

            user = new ApplicationUser
            {
                UserName = userName,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}