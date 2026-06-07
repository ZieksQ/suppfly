using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Users.ChangePassword;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/api/users/{id}/password", async (
          Guid id,
          ChangePasswordRequest request,
          ISender sender,
          CancellationToken cancellationToken) =>
      {
        var command = new Command(id, request.CurrentPassword, request.NewPassword);
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
          ? Results.Accepted("GetUserById", result.ToResponse("Password changed successfully."))
          : Results.BadRequest(result.ToResponse());
      }
    )
    .WithTags("Users");
  }
}
