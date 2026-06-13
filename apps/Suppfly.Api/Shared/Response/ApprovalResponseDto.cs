using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.DTOs;

namespace Suppfly.Api.Shared.Response;

public record ApprovalResponseDto(
  Guid Id,
  CompanyResponseDto? Company,
  UserResponseDto? RequestedByUser,
  ApprovalStatus Status,
  string? Notes,
  Guid? ReviewedByUserId,
  DateTime? ReviewedAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
