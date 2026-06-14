using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Response;

namespace Suppfly.Api.Features.Users.GetUser;

public record Response(
  Guid Id,
  string Email,
  string FullName,
  UserRole Role,
  UserStatus Status,
  Guid? CompanyId,
  CompanyResponseDto? Company,
  DateTime? LastLoginAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
