using Movies.API.Models;

namespace Movies.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Movie> Movies =>
        new List<Movie>()
        {
            new Movie(1, "The Shawshank Redemption", "Drama", "R", new DateTime(1994, 9, 22), "https://example.com/shawshank.jpg", "admin"),
            new Movie(2, "The Godfather", "Crime", "R", new DateTime(1972, 3, 24), "https://example.com/godfather.jpg", "admin"),
            new Movie(3, "The Dark Knight", "Action", "PG-13", new DateTime(2008, 7, 18), "https://example.com/darkknight.jpg", "admin"),
            new Movie(4, "Pulp Fiction", "Crime", "R", new DateTime(1994, 10, 14), "https://example.com/pulpfiction.jpg", "admin"),
            new Movie(5, "Forrest Gump", "Drama", "PG-13", new DateTime(1994, 7, 6), "https://example.com/forrestgump.jpg", "admin"),
            new Movie(6, "Inception", "Sci-Fi", "PG-13", new DateTime(2010, 7, 16), "https://example.com/inception.jpg", "admin"),
            new Movie(7, "Fight Club", "Drama", "R", new DateTime(1999, 10, 15), "https://example.com/fightclub.jpg", "admin"),
            new Movie(8, "The Matrix", "Sci-Fi", "R", new DateTime(1999, 3, 31), "https://example.com/matrix.jpg", "admin"),
            new Movie(9, "The Lord of the Rings: The Fellowship of the Ring", "Fantasy", "PG-13", new DateTime(2001, 12, 19), "https://example.com/lordoftherings.jpg", "admin"),
            new Movie(10, "The Social Network", "Biography", "PG-13", new DateTime(2010, 10, 1), "https://example.com/socialnetwork.jpg", "admin")
        };
}
