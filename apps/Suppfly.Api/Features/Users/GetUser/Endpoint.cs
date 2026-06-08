using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Users.GetUser;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/api/users/{id}", async (
          Guid id,
          bool? includeUser,
          ISender sender,
          CancellationToken cancellationToken) =>
      {
        var query = new Query(id, IncludeUser: includeUser ?? false);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
          ? Results.Ok(result.ToResponse("Get user successfully."))
          : Results.NotFound(result.ToResponse());

        // return result
        //   .OnSuccess(_ => Results.Ok(result.ToResponse("Get user information successfully.")))
        //   .OnFailure(_ => Results.)
      })
    .WithName("GetUserById")
    .WithTags("Users");
  }
}
