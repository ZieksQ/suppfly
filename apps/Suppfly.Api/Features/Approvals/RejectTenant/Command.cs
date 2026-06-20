using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.RejectTenant;

public sealed record Command(
  Guid Id,
  string? RejectionReason
) : IRequest<Result<Response>>;

public sealed record Response(
  Guid CompanyId,
  string Status,
  string? RejectionReason,
  Guid? RejectedByUserId,
  DateTime? RejectedAt
);
