using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Features.Users.UpdateProfile;

public record Response(
  Guid Id,
  string Email,
  string FirstName,
  string LastName,
  UserRole Role,
  UserStatus Status,
  Guid CompanyId,
  DateTime? LastLoginAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
