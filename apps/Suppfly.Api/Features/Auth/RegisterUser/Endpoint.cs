using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Auth.RegisterUser;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/auth/register", async (
          Command command,
          ISender sender,
          CancellationToken cancellationToken) =>
    {
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.AcceptedAtRoute("GetUserById", new { id = result.Value }, result.ToResponse("Successfully Registered User."))
        : Results.Conflict(result.ToResponse("Error Occured."));
    })
    .AllowAnonymous() // register don't need token
    .WithTags("Auth");
  }
}
