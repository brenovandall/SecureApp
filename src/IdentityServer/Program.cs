using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServerInjection();

var app = builder.Build();

app.UseServerInjection();

app.Run();