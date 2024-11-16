using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data.Extensions;

public static class DatabaseExtensions
{
    public static async void InitializeDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        // await SeedIdentityServer(context);
    }

    public static async Task SeedIdentityServer(ApplicationDbContext context)
    {
        await SeedClients(context);
        await SeedIdentityResources(context);
        await SeedApiScopes(context);
    }

    public static async Task SeedClients(ApplicationDbContext context)
    {
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client);
            }

            context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }

    public static async Task SeedIdentityResources(ApplicationDbContext context)
    {
        if (!context.IdentityResources.Any())
        {
            foreach (var identityResource in Config.IdentityResources)
            {
                context.IdentityResources.Add(identityResource);
            }

            context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }

    public static async Task SeedApiScopes(ApplicationDbContext context)
    {
        if (!context.ApiScopes.Any())
        {
            foreach (var apiScope in Config.ApiScopes)
            {
                context.ApiScopes.Add(apiScope);
            }

            context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
