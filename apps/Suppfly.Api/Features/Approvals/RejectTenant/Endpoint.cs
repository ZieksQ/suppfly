using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Approvals.RejectTenant;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/v1/tenant/{id}/reject", async (
      Guid id,
      string? reason,
      ISender sender,
      CancellationToken cancellationToken) =>
    {
      var command = new Command(id, reason);
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Tenant request rejected."))
        : Results.BadRequest(result.ToResponse());
    })
    .RequireAuthorization("AdministrationUsers")
    .WithTags("Approval");
  }
}
