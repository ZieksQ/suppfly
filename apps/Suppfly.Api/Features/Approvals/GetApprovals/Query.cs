using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.DTOs;
using Suppfly.Api.Shared.Enums;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.GetApprovals;

public record Query(
  int PageNumber,
  int PageSize,
  ApprovalStatus? Status,
  string? Search,
  SortDirections? SortDirections,
  bool IncludeCompany,
  bool IncludeOwner,
  bool IncludeAll
) : IRequest<Result<PagedList<Response>>>;

public record Response(
  Guid Id,
  CompanyResponseDto? Company,
  UserResponseDto? RequestedByUser,
  ApprovalStatus Status,
  string? Notes,
  Guid? ReviewedByUserId,
  DateTime? ReviewedAt,
  DateTime UpdatedAt,
  DateTime CreatedAt
);
