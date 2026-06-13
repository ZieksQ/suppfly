using MediatR;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.ApproveRequest;

public record Command(
  Guid Id,
  string? Notes
) : IRequest<Result<ApprovalResponseDto>>;
