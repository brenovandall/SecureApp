using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServerInjection(builder.Configuration);

var app = builder.Build();

app.UseServerInjection();

app.Run();
