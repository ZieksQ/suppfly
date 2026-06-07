using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Companies.GetCompany;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/api/companies/{id}", async (
          Guid id,
          bool? includeUsers,
          ISender sender,
          CancellationToken cancellationToken) =>
    {
      var query = new Query(id, includeUsers ?? false);
      var result = await sender.Send(query, cancellationToken);

      return result.IsSuccess
        ? Results.Ok(result.ToResponse("Get company details successfully."))
        : Results.NotFound(result.ToResponse());
    })
      .WithName("GetCompanyWithId")
      .WithTags("Company");
  }
}
