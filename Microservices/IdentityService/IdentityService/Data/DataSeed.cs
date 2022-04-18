using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityService.Data;

public static class DataSeed
{
    public static void FillTables(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
        }

        context.SaveChanges();
    }
}