using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.RejectRequest;

public class Handler : IRequestHandler<Command, Result<ApprovalResponseDto>>
{
  private readonly AppDbContext _db;
  private readonly ICurrentUserContext _currentUserContext;

  public Handler(AppDbContext db, ICurrentUserContext currentUserContext)
  {
    _db = db;
    _currentUserContext = currentUserContext;
  }

  public async Task<Result<ApprovalResponseDto>> Handle(Command request, CancellationToken cancellationToken)
  {
    // var approvalRequest = await _db.CompanyApprovalRequests
    //   .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
    //
    // if (approvalRequest is null)
    //   return Result<ApprovalResponseDto>.Fail("Approval request does not exists.");
    //
    // approvalRequest.Approved(_currentUserContext.UserId, request.Notes);
    //
    return Result<ApprovalResponseDto>.Fail("ok");
  }
}
