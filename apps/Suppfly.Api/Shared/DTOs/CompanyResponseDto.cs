using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.DTOs;

public record CompanyResponseDto(
  Guid Id,
  string Name,
  string Slug,
  CompanyType Type,
  string? TaxId,
  CompanyStatus Status,
  CompanyTier Tier,
  DateTime? ApprovedAt,
  Guid? ApprovedByUserId,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
