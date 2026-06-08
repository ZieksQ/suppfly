using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Users.GetUser;

public class Handler : IRequestHandler<Query, Result<Response>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }
  public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
  {
    var query = await _db.Users
      .AsNoTracking()
      .Where(u => u.Id == request.Id)
      .Select(u => new Response(
            u.Id,
            u.Email,
            $"{u.FirstName} {u.LastName}",
            u.Role,
            u.Status,
            u.CompanyId,
            request.IncludeUser && u.Company != null
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
      .FirstOrDefaultAsync(cancellationToken);

    if (query is null)
      return Result<Response>.Fail("User does not exists.");

    return Result<Response>.Ok(query);
  }
}
