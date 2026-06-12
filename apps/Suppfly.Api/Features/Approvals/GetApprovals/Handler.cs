using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.DTOs;
using Suppfly.Api.Shared.Enums;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.GetApprovals;

// NOTE: Since this feature is only accessible for PlatformAdmin
// there is no need to use Handler / Business level Auth checking
// if needed add ICurrentUserContext
public class Handler : IRequestHandler<Query, Result<PagedList<Response>>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<PagedList<Response>>> Handle(Query request, CancellationToken cancellationToken)
  {
    int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
    int pageSize = request.PageSize > 50 ? 50 : request.PageSize;

    IQueryable<CompanyApprovalRequest> query = _db.CompanyApprovalRequests
      .AsNoTracking();

    if (request.Status is not null)
    {
      query = query.Where(apr => apr.Status == request.Status);
    }

    if (!string.IsNullOrWhiteSpace(request.Search))
    {
      query = query.Where(apr =>
          EF.Functions.ILike(apr.Company.Name, $"%{request.Search}%")
            || EF.Functions.ILike(apr.RequestedByUser.FirstName, $"%{request.Search}%")
            || EF.Functions.ILike(apr.RequestedByUser.LastName, $"%{request.Search}%"));
    }

    query = request.SortDirections switch
    {
      SortDirections.Asc => query.OrderBy(apr => apr.CreatedAt),
      _ => query.OrderByDescending(apr => apr.CreatedAt)
    };

    int totalRecords = await query.CountAsync(cancellationToken);

    var pendingApprovals = await query
      .Select(apr => new Response(
        apr.Id,
        request.IncludeCompany || request.IncludeAll
          ? new CompanyResponseDto(
              apr.CompanyId,
              apr.Company.Name,
              apr.Company.Slug,
              apr.Company.Type,
              apr.Company.TaxId,
              apr.Company.Status,
              apr.Company.Tier,
              apr.Company.ApprovedAt,
              apr.Company.ApprovedByUserId,
              apr.Company.CreatedAt,
              apr.Company.UpdatedAt) : null,
        request.IncludeOwner || request.IncludeAll
          ? new UserResponseDto(
            apr.RequestedByUser.Id,
            apr.RequestedByUser.FirstName,
            apr.RequestedByUser.LastName,
            apr.RequestedByUser.Email,
            apr.RequestedByUser.Role,
            apr.RequestedByUser.Status,
            apr.RequestedByUser.CompanyId,
            apr.RequestedByUser.LastLoginAt,
            apr.RequestedByUser.CreatedAt,
            apr.RequestedByUser.UpdatedAt) : null,
        apr.Status,
        apr.Notes,
        apr.RequestedByUserId,
        apr.ReviewedAt,
        apr.UpdatedAt,
        apr.CreatedAt))
      .ToListAsync(cancellationToken);

    var result = new PagedList<Response>(
        pendingApprovals,
        pageNumber,
        pageSize,
        totalRecords
    );

    return Result<PagedList<Response>>.Ok(result);
  }
}
