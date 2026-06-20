using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Approvals.GetPendingTenant;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/api/v1/tenant/pending", async (
      int? pageNumber,
      int? pageSize,
      ISender sender,
      CancellationToken cancellationToken) =>
    {
      var query = new Query(pageNumber ?? 1, pageSize ?? 15);
      var result = await sender.Send(query, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Successufully get pending tenants."))
        : Results.BadRequest(result.ToResponse("Error has occured."));
    })
    .RequireAuthorization("AdministrationUsers")
    .WithTags("Approval");
  }
}
