using AuthorizationServer.Data;
using OpenIddict.Abstractions;

namespace AuthorizationServer
{
    public class SeedDataService : IHostedService
    {
        private readonly IServiceProvider _services;

        public SeedDataService(IServiceProvider services) => _services = services;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();

            // Create database if needed
            var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            await userDbContext.Database.EnsureCreatedAsync(cancellationToken);

            // Register new clients
            var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            // You can register your own client. SPA app, for instance
            await AddPostmanClientAsync(applicationManager, cancellationToken);
        }

        private async Task AddPostmanClientAsync(IOpenIddictApplicationManager applicationManager, CancellationToken cancellationToken)
        {
            const string clientId = "client";
            if (await applicationManager.FindByClientIdAsync(clientId, cancellationToken) == null)
            {
                await applicationManager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                        {
                            ClientId = clientId,
                            Permissions =
                                {
                                    OpenIddictConstants.Permissions.Endpoints.Token,
                                    OpenIddictConstants.Permissions.GrantTypes.Password,
                                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",
                                }
                        },
                    cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}