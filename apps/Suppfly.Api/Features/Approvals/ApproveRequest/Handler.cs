using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.ApproveRequest;

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
    var approvalRequest = await _db.CompanyApprovalRequests
      .Include(apr => apr.Company)
      .Include(apr => apr.RequestedByUser)
      .FirstOrDefaultAsync(apr => apr.Id == request.Id, cancellationToken);

    if (approvalRequest is null)
      return Result<ApprovalResponseDto>.Fail("Approval request does not exists.");

    // Approved Requests
    approvalRequest.Approved(_currentUserContext.UserId, request.Notes);

    // Approved Company
    approvalRequest.Company.Status = CompanyStatus.Active;

    // Approved User
    approvalRequest.RequestedByUser.Status = UserStatus.Active;

    await _db.SaveChangesAsync(cancellationToken);

    var result = new ApprovalResponseDto(
        approvalRequest.Id,
        new CompanyResponseDto(
          approvalRequest.Company.Id,
          approvalRequest.Company.Name,
          approvalRequest.Company.Slug,
          approvalRequest.Company.Type.ToString(),
          approvalRequest.Company.TaxId,
          approvalRequest.Company.Status.ToString(),
          approvalRequest.Company.Tier.ToString(),
          approvalRequest.Company.ApprovedAt,
          approvalRequest.Company.ApprovedByUserId,
          approvalRequest.Company.CreatedAt,
          approvalRequest.Company.UpdatedAt),
        new UserResponseDto(
          approvalRequest.RequestedByUser.Id,
          approvalRequest.RequestedByUser.FirstName,
          approvalRequest.RequestedByUser.LastName,
          approvalRequest.RequestedByUser.Email,
          approvalRequest.RequestedByUser.Role.ToString(),
          approvalRequest.RequestedByUser.Status.ToString(),
          approvalRequest.RequestedByUser.CompanyId,
          approvalRequest.RequestedByUser.LastLoginAt,
          approvalRequest.RequestedByUser.CreatedAt,
          approvalRequest.RequestedByUser.UpdatedAt),
        approvalRequest.Status.ToString(),
        approvalRequest.Notes,
        approvalRequest.ReviewedByUserId,
        approvalRequest.ReviewedAt,
        approvalRequest.CreatedAt,
        approvalRequest.UpdatedAt);

    return Result<ApprovalResponseDto>.Ok(result);
  }
}
