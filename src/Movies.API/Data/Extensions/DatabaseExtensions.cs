using Microsoft.EntityFrameworkCore;

namespace Movies.API.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<MoviesAPIContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedDataAsync(context);
    }

    private static async Task SeedDataAsync(MoviesAPIContext context)
    {
        if (!await context.Movie.AnyAsync())
        {
            await context.Movie.AddRangeAsync(InitialData.Movies);
            await context.SaveChangesAsync();
        }
    }
}
