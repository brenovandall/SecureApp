using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    private readonly IHttpClientFactory _clientFactory;
    public MovieApiService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        var client = _clientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/Movies");

        var response = await client.SendAsync(request,HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(true);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);

        return await Task.FromResult(movies);
    }

    //public async Task<IEnumerable<Movie>> GetMovies()
    //{
    //    var credentials = new ClientCredentialsTokenRequest
    //    {
    //        Address = "https://localhost:5005/connect/token",
    //        ClientId = "movieClient",
    //        ClientSecret = "secret",
    //        Scope = "movieAPI"
    //    };

    //    var disco = await _client.GetDiscoveryDocumentAsync("https://localhost:5005");
    //    if (disco.IsError)
    //        return null!;

    //    var token = await _client.RequestClientCredentialsTokenAsync(credentials);
    //    if (token.IsError) 
    //        return null!;

    //    _apiClient.SetBearerToken(token.AccessToken!);

    //    var response = await _apiClient.GetAsync("/api/Movies");
    //    response.EnsureSuccessStatusCode();

    //    var content = await response.Content.ReadAsStringAsync();

    //    List<Movie> movies = [];
    //    movies = JsonConvert.DeserializeObject<List<Movie>>(content);

    //    if (!movies.Any())
    //        return [];

    //    return await Task.FromResult(movies);
    //}

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
