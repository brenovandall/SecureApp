using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IdentityServer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<IdentityResource> IdentityResources => Set<IdentityResource>();
    public DbSet<ApiScope> ApiScopes => Set<ApiScope>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiScope>().HasNoKey();
        modelBuilder.Entity<Client>().HasNoKey();
        modelBuilder.Entity<IdentityResource>().HasNoKey();

        modelBuilder.Entity<ApiScope>()
            .Property(p => p.Properties)
            .HasConversion(v => JsonConvert.SerializeObject(v),
                           v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v)!);

        modelBuilder.Entity<Client>()
            .Property(p => p.Properties)
            .HasConversion(v => JsonConvert.SerializeObject(v),
                           v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v)!);

        modelBuilder.Entity<IdentityResource>()
          .Property(p => p.Properties)
          .HasConversion(v => JsonConvert.SerializeObject(v),
                         v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v)!);

        base.OnModelCreating(modelBuilder);
    }
}
