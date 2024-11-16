using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public MovieApiService(IHttpClientFactory clientFactory, IHttpContextAccessor contentAccessor)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _contextAccessor = contentAccessor ?? throw new ArgumentNullException(nameof(contentAccessor));
    }

    public async Task<UserInfoViewModel> GetUserInfo()
    {
        var idpClient = _clientFactory.CreateClient("IDPClient");

        var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
        if (metaDataResponse.IsError)
            throw new HttpRequestException("Something went wrong while processing the request.");

        var token = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var userInfo = await idpClient.GetUserInfoAsync(
            new UserInfoRequest
            {
                Address = metaDataResponse.UserInfoEndpoint,
                Token = token
            });

        if (userInfo.IsError)
            throw new HttpRequestException("Something went wrong while processing the request.");

        var userInfoDictionary = new Dictionary<string, string>();

        foreach (var claim in userInfo.Claims)
        {
            userInfoDictionary.Add(claim.Type, claim.Value);
        }

        return new UserInfoViewModel(userInfoDictionary);
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        var client = _clientFactory.CreateClient("MovieAPIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/movies");

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
