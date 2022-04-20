using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Extensions
{
    public static class HostingExtension
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            const string connectionStringName = "DefaultConnection";
            var connectionString = builder.Configuration.GetConnectionString(connectionStringName);

            // Add identity configuration
            builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            // IdentityServer configuration
            builder.Services.AddIdentityServer()
                // This adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(
                    options =>
                    {
                        options.ConfigureDbContext = dbContextOptionsBuilder
                            => dbContextOptionsBuilder.UseSqlServer(
                                connectionString,
                                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(Program).Assembly.FullName));
                    })
                // This is something you will want in production to reduce load on and requests to the DB
                .AddConfigurationStoreCache()
                // This adds the operational data from DB (codes, tokens, consents)
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
                .AddAspNetIdentity<ApplicationUser>()
                // Only for dev purposes (https://identityserver4.readthedocs.io/en/latest/topics/startup.html#key-material)
                .AddDeveloperSigningCredential();
        builder.Services.AddControllersWithViews();
        }

        public static void ConfigurePipeline(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}