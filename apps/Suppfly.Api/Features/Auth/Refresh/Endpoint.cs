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
          ISender sender,
          HttpContext httpContext,
          IConfiguration config,
          IHostEnvironment env,
          CancellationToken cancellationToken) =>
    {
      var refreshToken = httpContext.Request.Cookies["refresh_token"];

      if (refreshToken is null)
      {
        Console.WriteLine("No refresh token recieved.");
        return Results.Forbid();
      }

      Console.WriteLine("Refresh Token recieved");

      var command = new Command(refreshToken);

      var result = await sender.Send(command, cancellationToken);

      if (result.IsFailure)
      {
        Console.WriteLine(result.Error);
        return Results.Forbid();
      }

      var jwtOptions = config.GetSection("Jwt");

      httpContext.Response.Cookies.Append(
          "access_token",
          result.Value!,
          CookieOptionsFactory.AccessToken(
            env,
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
