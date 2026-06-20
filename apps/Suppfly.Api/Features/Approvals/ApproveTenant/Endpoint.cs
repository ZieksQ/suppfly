using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Approvals.ApproveTenant;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    // NOTE: Why POST instead of PUT or PATCH?
    // RCP Style - makes it clear that approval is a business operation
    // and if in the future it also sends an Email and do different task 
    // its not like you are just changing status.
    app.MapPost("/api/v1/tenant/{id}/approve", async (
      Guid id,
      ISender sender,
      CancellationToken cancellationToken) =>
    {
      var command = new Command(id);
      var result = await sender.Send(command, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Tenant Approved."))
        : Results.BadRequest(result.ToResponse());
    })
    .RequireAuthorization("AdministrationUsers")
    .WithTags("Approval");
  }
}
