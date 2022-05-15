using AuthorizationServer.Constants;
using AuthorizationServer.Data;
using OpenIddict.Abstractions;

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
            if (await applicationManager.FindByClientIdAsync(ClientNames.SpaClient, cancellationToken) == null)
            {
                await applicationManager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = ClientNames.SpaClient,

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
            if (await applicationManager.FindByClientIdAsync(ClientNames.BackendClient, cancellationToken) == null)
            {
                await applicationManager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = ClientNames.BackendClient,
                        ClientSecret = "secret",
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Introspection
                        }
                    },
                    cancellationToken);
            }
        }
    }
}