using Carter;
using MediatR;

namespace Suppfly.Api.Features.Companies.CreateCompany;

public class Endpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/company", async (
      Command command,
      ISender sender,
      CancellationToken cancellationToken) =>
      {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
          ? Results.Created($"/api/company/{result.Value!.Id}", result.Value)
          : Results.BadRequest(new { error = result.Error });
      })
      .WithName("CreateCompany")
      .WithTags("Companies");
  }
}
