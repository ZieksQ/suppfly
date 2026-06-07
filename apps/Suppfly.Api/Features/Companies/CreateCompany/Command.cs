using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.CreateCompany;

public record Command(
  string Name,
  string Slug,
  CompanyType Type,
  string? TaxId,
  CompanyTier Tier
) : IRequest<Result<Response>>;
