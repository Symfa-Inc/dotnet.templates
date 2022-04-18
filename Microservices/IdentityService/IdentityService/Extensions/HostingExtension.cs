using Microsoft.EntityFrameworkCore;

namespace IdentityService.Extensions
{
    public static class HostingExtension
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            const string connectionStringName = "DefaultConnection";
            var connectionString = builder.Configuration.GetConnectionString(connectionStringName);
            builder.Services.AddIdentityServer()
                // this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(
                    options =>
                    {
                        options.ConfigureDbContext = dbContextOptionsBuilder
                            => dbContextOptionsBuilder.UseSqlServer(
                                connectionString,
                                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(Program).Assembly.FullName));
                    })
                // this is something you will want in production to reduce load on and requests to the DB
                .AddConfigurationStoreCache()
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(
                    options =>
                    {
                        options.ConfigureDbContext = dbContextOptionsBuilder
                            => dbContextOptionsBuilder.UseSqlServer(
                                connectionString,
                                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(Program).Assembly.FullName));

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;

                        // interval in seconds (default is 3600)
                        options.TokenCleanupInterval = 3600;
                    })
                // only for dev purposes (https://identityserver4.readthedocs.io/en/latest/topics/startup.html#key-material)
                .AddDeveloperSigningCredential();
        }

        public static void ConfigurePipeline(this WebApplication app)
        {
            app.UseIdentityServer();
        }
    }
}