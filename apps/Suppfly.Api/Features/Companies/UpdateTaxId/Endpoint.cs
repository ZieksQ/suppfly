using Carter;
using MediatR;
using Suppfly.Api.Shared.Extensions;

namespace Suppfly.Api.Features.Companies.UpdateTaxId;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/api/companies/{id}/tax", async (
          Guid id,
          RequestDto request,
          ISender sender,
          CancellationToken cancellationToken) =>
        {
          var command = new Command(id, request.TaxId);
          var result = await sender.Send(command, cancellationToken);

          return result.IsSuccess
            ? Results.NoContent()
            : Results.BadRequest(result.ToResponse());
        })
      .WithTags("Companies");
  }
}
