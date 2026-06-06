using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared;
using Suppfly.Api.Shared.Response;

namespace Suppfly.Api.Features.Users.GetUsersList;

public class Handler : IRequestHandler<Query, Result<PagedList<Response>>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<PagedList<Response>>> Handle(Query request, CancellationToken cancellationToken)
  {
    // constraints limit
    int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
    int pageSize = request.PageSize > 50 ? 50 : request.PageSize;

    // sort by name as default
    var query = _db.Users
      .Include(u => u.Company)
      .OrderBy(u => u.LastName)
        .ThenBy(u => u.FirstName)
      .AsNoTracking();

    int totalRecords = await query.CountAsync(cancellationToken);

    var users = await query
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .Select(u => new Response(
        u.Id,
        u.Email,
        $"{u.FirstName} {u.LastName}",
        u.Role,
        u.Status,
        u.CompanyId,
        request.IncludeCompany && u.Company != null
          ? new CompanyResponseDto(
            u.Company.Name,
            u.Company.Slug,
            u.Company.Type,
            u.Company.TaxId,
            u.Company.Status,
            u.Company.Tier,
            u.Company.ApprovedAt,
            u.Company.ApprovedByUserId,
            u.Company.CreatedAt,
            u.Company.UpdatedAt
            )
          : null,
        u.LastLoginAt,
        u.CreatedAt,
        u.UpdatedAt
      ))
      .ToListAsync(cancellationToken);

    // int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    var response = new PagedList<Response>(
      users,
      pageNumber,
      pageSize,
      totalRecords
    );

    return Result<PagedList<Response>>.Ok(response);
  }
}
