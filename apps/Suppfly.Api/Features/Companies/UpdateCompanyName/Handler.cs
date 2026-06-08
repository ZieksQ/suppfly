using MediatR;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.UpdateCompanyName;

public class Handler : IRequestHandler<Command, Result>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }
  public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
  {
    var company = await _db.Companies
      .FindAsync([request.Id], cancellationToken);

    if (company is null)
      return Result.Fail("Comapany does not exists.");

    company.Name = request.Name;
    company.Slug = request.Slug;

    await _db.SaveChangesAsync(cancellationToken);

    return Result.Ok();
  }
}
