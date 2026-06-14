using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.GetCompany;

public record Query(
  Guid Id,
  bool IncludeUsers
) : IRequest<Result<Response>>;

public record Response(
  Guid Id,
  string Name,
  string Slug,
  CompanyType Type,
  string? TaxId,
  CompanyStatus Status,
  CompanyTier Tier,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
