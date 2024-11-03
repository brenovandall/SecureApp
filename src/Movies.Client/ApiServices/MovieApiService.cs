using Movies.Client.Models;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    public async Task<IEnumerable<Movie>> GetMovies()
    {
        List<Movie> movies = [];
        movies.Add(
            new Movie(1, "The Shawshank Redemption", "Drama", "R", new DateTime(1994, 9, 22), "https://example.com/shawshank.jpg", "admin"));

        return await Task.FromResult(movies);
    }

    public Task<Movie> GetMovie(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> CreateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMovie(int id)
    {
        throw new NotImplementedException();
    }
}
