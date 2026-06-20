using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Auth.VerifyUserEmail;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    // WARNING: using :id in endpoint has its security risk
    // anyone can guess the url pattern and verify random emails,
    // in development this is perfect but in production this is nightmare.
    // TODO: to reduce security risk add Token to verify its valid.
    // Add Verification Token in the db or cache then use it as query params
    // /api/v1/auth/123/verify?token=qwerty123...
    app.MapPut("/api/v1/auth/{id}/verify", async (
      Guid id,
      ISender sender,
      CancellationToken cancellationToken) =>
    {
      var command = new Command(id);
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Successfully verified email."))
        : Results.BadRequest(result.ToResponse());
    })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
