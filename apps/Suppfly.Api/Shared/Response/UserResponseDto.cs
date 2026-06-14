namespace Suppfly.Api.Shared.Response;

public record UserResponseDto(
  Guid Id,
  string FirstName,
  string LastName,
  string Email,
  string Role,
  string Status,
  Guid? CompanyId,
  DateTime? LastLoginAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
