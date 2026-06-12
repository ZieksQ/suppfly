using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.DTOs;

public record UserResponseDto(
  Guid Id,
  string FirstName,
  string LastName,
  string Email,
  UserRole Role,
  UserStatus Status,
  Guid? CompanyId,
  DateTime? LastLoginAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
