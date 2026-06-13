using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Approvals.ApproveRequest;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/api/v1/admin/approvals/{id}/approved", async (
          Guid id,
          string? Notes,
          ISender sender,
          CancellationToken cancellationToken) =>
    {
      var command = new Command(id, Notes);
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("New company request accepted."))
        : Results.BadRequest(result.ToResponse());
    })
    .RequireAuthorization("AdminOnly")
    .WithTags("Approvals");
  }
}
