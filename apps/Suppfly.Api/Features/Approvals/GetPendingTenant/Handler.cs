using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.GetPendingTenant;

public class Handler : IRequestHandler<Query, Result<PagedList<Response>>>
{
  private readonly AppDbContext _context;

  public Handler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Result<PagedList<Response>>> Handle(Query request, CancellationToken cancellationToken)
  {

    int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
    int pageSize = request.PageSize > 50 ? 50 : request.PageSize;

    var query = _context.Companies
      .AsNoTracking()
      .Where(c => c.Status == CompanyStatus.Pending_Approval)
      .AsQueryable();

    int totalRecords = await query.CountAsync(cancellationToken);

    var items = await query.Select(c => new Response(
      new CompanyDto(
        Name: c.Name,
        Status: c.Status.ToString(),
        new UserDto(
          Id: c.CreatedByUser.Id,
          Email: c.CreatedByUser.Email,
          FirstName: c.CreatedByUser.FirstName,
          LastName: c.CreatedByUser.LastName),
        CreatedAt: c.CreatedAt)))
    .ToListAsync(cancellationToken);

    var result = new PagedList<Response>(
        items: items,
        pageNumber: pageNumber,
        pageSize: pageSize,
        totalRecords: totalRecords);

    return Result<PagedList<Response>>.Ok(result);
  }
}
