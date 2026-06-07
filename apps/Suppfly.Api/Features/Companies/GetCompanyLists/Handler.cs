using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.GetCompanyLists;

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

    var query = _db.Companies
      .OrderBy(u => u.Name)
      .AsNoTracking();

    int totalRecords = await query.CountAsync(cancellationToken);

    var companies = await query
      .Select(c => new Response(
        c.Id,
        c.Name,
        c.Slug,
        c.Type,
        c.TaxId,
        c.Status,
        c.Tier,
        c.ApprovedByUserId,
        c.ApprovedAt,
        c.CreatedAt,
        c.UpdatedAt
      ))
      .ToListAsync(cancellationToken);

    var result = new PagedList<Response>(
        companies,
        pageNumber,
        pageSize,
        totalRecords
    );

    return Result<PagedList<Response>>.Ok(result);
  }
}
