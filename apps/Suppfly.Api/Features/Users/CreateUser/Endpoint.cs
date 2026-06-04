using Carter;
using MediatR;

namespace Suppfly.Api.Features.Users.CreateUser;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/users", async (
     Command command,
     ISender sender,
     CancellationToken cancellationToken) =>
      {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
          ? Results.Created($"/api/users/{result.Value!.Id}", result.Value)
          : Results.BadRequest(new { error = result.Error });
      })
    .WithName("CreateUser")
    .WithTags("Users");
  }
}
