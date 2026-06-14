using MediatR;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.RejectRequest;

public record Command(
  Guid Id,
  string? Notes
) : IRequest<Result<ApprovalResponseDto>>;
