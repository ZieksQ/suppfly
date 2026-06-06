using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Users.GetUsersList;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/api/users", async (
      int? pageNumber,
      int? pageSize,
      bool? includeCompany,
      ISender sender,
      CancellationToken cancellationToken) =>
      {
        var query = new Query(pageNumber ?? 1, pageSize ?? 10, includeCompany ?? false);
        var result = await sender.Send(query, cancellationToken);
        return result.IsSuccess
          ? Results.Ok(result.ToResponse("Get users list successfully."))
          : Results.BadRequest(result.ToResponse());
      })
    .WithName("GetUsersList")
    .WithTags("Users");
  }
}
