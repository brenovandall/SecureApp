using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityServerHost;

namespace IdentityServer;

public static class DependencyInjection
{
    public static IServiceCollection AddServerInjection(this IServiceCollection services)
    {
        services.AddRazorPages();

        services.AddIdentityServer(options =>
        {
            options.KeyManagement.Enabled = false;
        }).AddInMemoryClients(Config.Clients)
          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddInMemoryIdentityResources(Config.IdentityResources)
          .AddTestUsers(Config.TestUsers)
          .AddDeveloperSigningCredential();

        return services;
    }

    public static WebApplication UseServerInjection(this WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }
}
