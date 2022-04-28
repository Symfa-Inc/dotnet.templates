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

            // You can register your own client(s). SPA app, for instance
            await AddSpaClientAsync(applicationManager, cancellationToken);
        }

        private async Task AddSpaClientAsync(IOpenIddictApplicationManager applicationManager, CancellationToken cancellationToken)
        {
            const string clientId = "spaClient";
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
                                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                                }
                        },
                    cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}