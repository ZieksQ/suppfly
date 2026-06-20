using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.RejectTenant;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _context;
  private readonly ICurrentUserContext _currentUser;

  public Handler(
      AppDbContext context,
      ICurrentUserContext currentUser)
  {
    _context = context;
    _currentUser = currentUser;
  }

  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var company = await _context.Companies
      .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

    if (company is null)
      return Result<Response>.Fail("Company does not exists.");

    company.Rejected(_currentUser.UserId, request.RejectionReason);

    await _context.SaveChangesAsync(cancellationToken);

    var result = new Response(
        CompanyId: company.Id,
        Status: company.Status.ToString(),
        RejectionReason: company.RejectionReason,
        RejectedByUserId: company.RejectedByUserId,
        RejectedAt: company.RejectedAt
        );

    return Result<Response>.Ok(result);
  }
}
