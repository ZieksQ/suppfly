using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Auth.RegisterB2BAccount;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/account/register", async (
          Command command,
          ISender sender,
          CancellationToken cancellationToken) =>
        {
          var result = await sender.Send(command, cancellationToken);

          return result.IsSuccess
            ? Results.CreatedAtRoute(
                "GetUserById",
                new { id = result.Value },
                result.ToResponse("Registered Successfully."))
            : Results.BadRequest(result.ToResponse());
        })
    .AllowAnonymous()
    .WithTags("Auth");
  }
}
