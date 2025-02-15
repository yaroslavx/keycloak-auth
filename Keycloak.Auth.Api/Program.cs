using System.Security.Claims;
using Keycloak.Auth.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) =>
{
    return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();

app.UseAuthentication();

app.UseAuthorization();

app.Run();