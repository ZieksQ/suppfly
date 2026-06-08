using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.UpdateTaxId;

public record Command(
  Guid Id,
  string TaxId
) : IRequest<Result>;

public record RequestDto(string TaxId);
