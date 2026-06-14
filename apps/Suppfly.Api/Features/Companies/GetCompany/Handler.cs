using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.GetCompany;

public class Handler : IRequestHandler<Query, Result<Response>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
  {
    var company = await _db.Companies
      .AsNoTracking()
      .Where(c => c.Id == request.Id)
      .Select(c => new Response(
        c.Id,
        c.Name,
        c.Slug,
        c.Type,
        c.TaxId,
        c.Status,
        c.Tier,
        c.CreatedAt,
        c.UpdatedAt
      ))
      .FirstOrDefaultAsync(cancellationToken);

    if (company is null)
      return Result<Response>.Fail("Company does not exists.");

    return Result<Response>.Ok(company);
  }
}
