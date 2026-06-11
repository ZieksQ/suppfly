using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Auth.LoginUser;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/login", async (
          Command command,
          ISender sender,
          CancellationToken cancellationToken) =>
    {
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Login Successfully."))
        : Results.BadRequest(result.ToResponse());
    })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
