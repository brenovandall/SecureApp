using Movies.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddClientServices();

var app = builder.Build();

app.UseClientServices();

app.Run();
