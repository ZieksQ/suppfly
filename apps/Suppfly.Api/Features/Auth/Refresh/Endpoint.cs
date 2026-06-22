using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Auth.Refresh;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/auth/refresh", async (
          Command command,
          ISender sender,
          CancellationToken cancellationToken) =>
    {
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Successfully validate refresh token."))
        : Results.Forbid();
    })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
