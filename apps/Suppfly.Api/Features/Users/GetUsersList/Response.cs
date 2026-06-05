using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Features.Users.GetUsersList;

public record Response(
  Guid Id,
  string Email,
  string FullName,
  UserRole Role,
  UserStatus Status,
  Guid CompanyId,
  CompanyResponseDto? Company,
  DateTime? LastLoginAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);

public record CompanyResponseDto(
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
