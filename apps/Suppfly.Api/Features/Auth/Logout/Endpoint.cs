using Carter;
using MediatR;

namespace Suppfly.Api.Features.Auth.Logout;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/auth/logout", async (
      HttpContext httpContext,
      ISender sender) =>
    {
      var refreshToken = httpContext.Request.Cookies["refresh_token"];

      if (!string.IsNullOrWhiteSpace(refreshToken))
      {
        await sender.Send(new Command(refreshToken));
      }

      httpContext.Response.Cookies.Delete("access_token");
      httpContext.Response.Cookies.Delete("refresh_token");

      return Results.NoContent();
    })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
