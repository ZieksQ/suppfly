using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Companies.GetCompanyLists;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/api/companies", async (
      int? pageNumber,
      int? pageSize,
      bool? includeUsers,
      ISender sender,
      CancellationToken cancellationToken) =>
      {
        var query = new Query(pageNumber ?? 1, pageSize ?? 10, includeUsers ?? false);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
          ? Results.Ok(result.ToResponse("Get company lists successfully."))
          : Results.BadRequest(result.ToResponse());
      })
      .WithName("GetCompanyLists")
      .WithTags("Company");
  }
}
