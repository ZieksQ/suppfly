namespace Suppfly.Api.Shared.Response;

public record CompanyResponseDto(
  Guid Id,
  string Name,
  string Slug,
  string Type,
  string? TaxId,
  string Status,
  string Tier,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
