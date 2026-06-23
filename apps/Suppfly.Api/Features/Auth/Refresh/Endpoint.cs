using Carter;
using MediatR;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Utils;

namespace Suppfly.Api.Features.Auth.Refresh;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/auth/refresh", async (
          Command command,
          ISender sender,
          HttpContext httpContext,
          IConfiguration config,
          CancellationToken cancellationToken) =>
    {
      var result = await sender.Send(command, cancellationToken);

      if (result.IsFailure)
      {
        return Results.Forbid();
      }

      var jwtOptions = config.GetSection("Jwt");

      httpContext.Response.Cookies.Append(
          "access_token",
          result.Value!,
          CookieOptionsFactory.AccessToken(
            int.Parse(jwtOptions["AccessTokenExpiryMinutes"]!)));

      return Results.Ok(new BaseResponse(
            Success: true,
            Message: "Successfully Refresh Token",
            Errors: null));
    })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
