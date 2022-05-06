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
            await RegisterClientsAsync(cancellationToken, applicationManager);
        }

        private async Task RegisterClientsAsync(CancellationToken cancellationToken, IOpenIddictApplicationManager applicationManager)
        {
            // You can register your own client(s). SPA app, for instance
            await AddSpaClientAsync(applicationManager, cancellationToken);
            await AddBackendClientAsync(applicationManager, cancellationToken);
        }

        private async Task AddSpaClientAsync(IOpenIddictApplicationManager applicationManager, CancellationToken cancellationToken)
        {
            if (await applicationManager.FindByClientIdAsync(ClientNames.SpaClient, cancellationToken) == null)
            {
                await applicationManager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = ClientNames.SpaClient,
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.Endpoints.Revocation,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Email
                        }
                    },
                    cancellationToken);
            }
        }

        private async Task AddBackendClientAsync(IOpenIddictApplicationManager applicationManager, CancellationToken cancellationToken)
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

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}