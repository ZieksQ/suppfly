using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Features.Companies.CreateCompany;

public record Response(
  Guid Id,
  string Name,
  string Slug,
  CompanyType Type,
  string? TaxId,
  CompanyStatus Status,
  CompanyTier Tier,
  DateTime CreatedAt
);
