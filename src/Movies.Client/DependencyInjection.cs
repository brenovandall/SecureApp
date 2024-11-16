using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Movies.Client.HttpHandlers;

namespace Movies.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllersWithViews();

        services.AddScoped<IMovieApiService, MovieApiService>();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.AccessDeniedPath = "/Movies/AccessDenied";
        })
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.Authority = "https://localhost:5005";

            options.ClientId = "movies_mvc_client";
            options.ClientSecret = "secret";
            options.ResponseType = "code id_token";
            //options.ResponseType = "code";

            //options.Scope.Add("openid");
            //options.Scope.Add("profile");
            options.Scope.Add("address");
            options.Scope.Add("email");
            options.Scope.Add("movieAPI");
            options.Scope.Add("roles");

            options.ClaimActions.MapUniqueJsonKey("role", "role");

            options.SaveTokens = true;

            options.GetClaimsFromUserInfoEndpoint = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = JwtClaimTypes.GivenName,
                RoleClaimType = JwtClaimTypes.Role
            };
        });

        services.AddTransient<AuthenticationDelegatingHandler>();

        services.AddHttpClient("MovieAPIClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5010");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        services.AddHttpClient("IDPClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5005");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        services.AddHttpContextAccessor();

        //services.AddSingleton(new ClientCredentialsTokenRequest
        //{
        //    Address = "https://localhost:5005/connect/token",
        //    ClientId = "movieClient",
        //    ClientSecret = "secret",
        //    Scope = "movieAPI"
        //});

        return services;
    }

    public static WebApplication UseClientServices(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}
