using MediatR;
using Suppfly.Api.Domain;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Features.Companies.CreateCompany;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var company = Company.Create(
        request.Name,
        request.Slug,
        request.Type,
        request.TaxId,
        request.Tier
    );

    _db.Companies.Add(company);
    await _db.SaveChangesAsync(cancellationToken);

    var response = new Response(
        company.Id,
        company.Name,
        company.Slug,
        company.Type,
        company.TaxId,
        company.Status,
        company.Tier,
        company.CreatedAt
    );

    return Result<Response>.Ok(response);
  }
}
