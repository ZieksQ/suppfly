using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Auth.Login;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/auth/login", async (
      Command command,
      ISender sender,
      CancellationToken cancellationToken) =>
    {
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Successfully Login"))
        : Results.BadRequest(result.ToResponse());
    })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
