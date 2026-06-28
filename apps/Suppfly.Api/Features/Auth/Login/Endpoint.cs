using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Utils;

namespace Suppfly.Api.Features.Auth.Login;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/auth/login", async (
      Command command,
      ISender sender,
      HttpContext httpContext,
      IConfiguration config,
      IHostEnvironment env,
      CancellationToken cancellationToken) =>
    {
      var result = await sender.Send(command, cancellationToken);

      var jwtOptions = config.GetSection("Jwt");

      // store access and refresh token in httpOnly Cookies
      if (result.IsFailure)
      {
        // return Results.Unauthorized();
        //
        // return Results.Problem(
        //     title: "Authentication Failed.",
        //     detail: result.Error,
        //     statusCode: StatusCodes.Status401Unauthorized);
        return TypedResults.Json(
            result.ToResponse("Authentication Failed."),
            statusCode: StatusCodes.Status401Unauthorized);
      }

      httpContext.Response.Cookies.Append(
        "access_token",
        result.Value!.AccessToken,
        CookieOptionsFactory.AccessToken(
          env,
          int.Parse(jwtOptions["AccessTokenExpiryMinutes"]!)));

      httpContext.Response.Cookies.Append(
        "refresh_token",
        result.Value!.RefreshToken,
        CookieOptionsFactory.RefreshToken(
          env,
          int.Parse(jwtOptions["RefreshTokenExpiryDays"]!)));

      // TODO: Add HATEOAS for links like /me
      return Results.Ok(new BaseResponse(
            Success: true,
            Message: "Login Successfully",
            Errors: null));
    })
    .AllowAnonymous()
    .WithName("Login")
    .WithTags("Auth");
  }
}
