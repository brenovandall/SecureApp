using IdentityServer.Data;
using IdentityServer.Data.Extensions;
using IdentityServerHost;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer;

public static class DependencyInjection
{
    public static IServiceCollection AddServerInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("db_connection"));
        });

        var assembly = Assembly.GetExecutingAssembly().GetName().Name;

        services.AddIdentityServer(options =>
        {
            options.KeyManagement.Enabled = false;
        })
          //.AddConfigurationStore(options =>
          //{
          //    options.ConfigureDbContext = b => b.UseSqlServer(
          //        configuration.GetConnectionString("db_connection"), sql => sql.MigrationsAssembly(assembly));
          //})
          .AddInMemoryClients(Config.Clients)
          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddInMemoryIdentityResources(Config.IdentityResources)
          //.AddConfigurationStoreCache()
          .AddTestUsers(TestUsers.Users)
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

        // app.InitializeDatabase();

        return app;
    }
}
