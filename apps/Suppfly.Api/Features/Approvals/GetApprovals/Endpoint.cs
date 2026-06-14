using Carter;
using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Enums;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Approvals.GetApprovals;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/api/v1/admin/approvals", async (
      ApprovalStatus? status,
      string? search,
      SortDirections? sort,
      int? pageNumber,
      int? pageSize,
      bool? includeCompany,
      bool? includeOwner,
      bool? includeAll,
      ISender sender,
      CancellationToken cancellationToken) =>
      {
        var query = new Query(
            pageNumber ?? 1,
            pageSize ?? 10,
            status,
            search,
            sort,
            includeCompany ?? false,
            includeOwner ?? false,
            includeAll ?? false);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
          ? Results.Ok(result.ToPagedResponse("Get Approvals Successfully."))
          : Results.BadRequest(result.ToPagedResponse());
      })
    // .RequireAuthorization(new AuthorizeAttribute { Roles = Roles.PlatformAdmin })
    .RequireAuthorization("AdminOnly")
    .WithTags("Approvals");
  }
}
