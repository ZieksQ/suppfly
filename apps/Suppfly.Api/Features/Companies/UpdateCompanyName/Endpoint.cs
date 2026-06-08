using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Companies.UpdateCompanyName;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/api/companies/{id}/name", async (
          Guid id,
          RequestDto request,
          ISender sender,
          CancellationToken cancellationToken) =>
      {
        var command = new Command(id, request.Name, request.Slug);
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
          ? Results.NoContent()
          : Results.BadRequest(result.ToResponse());
      })
      .WithTags("Companies");
  }
}
