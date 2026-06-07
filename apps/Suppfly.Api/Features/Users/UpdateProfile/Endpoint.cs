using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

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
        var command = new Command(
            id,
            request.FirstName,
            request.LastName,
            request.Email
        );

        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
          ? Results.Ok(result.ToResponse("Profile updated successfully."))
          : Results.BadRequest(result.ToResponse());
      }
    )
    .WithTags("Users");
  }
}
