using Movies.Client.Models;

namespace Movies.Client.ApiServices;

public interface IMovieApiService
{
    Task<UserInfoViewModel> GetUserInfo();
    Task<IEnumerable<Movie>> GetMovies();
    Task<Movie> GetMovie(string id);
    Task<Movie> CreateMovie(Movie movie);
    Task<Movie> UpdateMovie(Movie movie);
    Task DeleteMovie(int id);
}
