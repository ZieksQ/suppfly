using Carter;
using MediatR;

namespace Suppfly.Api.Features.Users.UpdateProfile;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/api/users/{id}", async (
          Guid id,
          UpdateProfileRequest request,
          ISender sender,
          CancellationToken cancellationToken) =>
      {
        var query = new Command(
            id,
            request.FirstName,
            request.LastName,
            request.Email
        );

        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
          ? Results.Ok(result.Value)
          : Results.BadRequest(new { error = result.Error });
      }
    )
    .WithTags("Users");
  }
}
