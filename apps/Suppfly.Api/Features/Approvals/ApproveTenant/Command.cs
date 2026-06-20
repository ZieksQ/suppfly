using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.ApproveTenant;

public sealed record Command(
  Guid Id
) : IRequest<Result<Response>>;

public sealed record Response(
  Guid CompanyId,
  string CompanyStatus,
  Guid? ApprovedByUserId,
  DateTime? ApprovedAt
);
