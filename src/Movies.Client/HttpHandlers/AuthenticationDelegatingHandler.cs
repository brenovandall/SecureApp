using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    //private readonly IHttpClientFactory _httpClientFactory;
    //private readonly ClientCredentialsTokenRequest _clientCredentialsTokenRequest;
    //public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest clientCredentialsTokenRequest)
    //{
    //    _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    //    _clientCredentialsTokenRequest = clientCredentialsTokenRequest ?? throw new ArgumentNullException(nameof(clientCredentialsTokenRequest));
    //}
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //var client = _httpClientFactory.CreateClient("IDPCLient");

        //var token = await client.RequestClientCredentialsTokenAsync(_clientCredentialsTokenRequest);
        //if (token.IsError)
        //    throw new HttpRequestException("Something went wrong while trying to request the access token!");

        //request.SetBearerToken(token.AccessToken!);

        var token = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        if (!string.IsNullOrEmpty(token))
        {
            request.SetBearerToken(token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
