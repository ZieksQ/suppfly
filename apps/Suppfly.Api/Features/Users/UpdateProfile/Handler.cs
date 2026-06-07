using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Users.UpdateProfile;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var user = await _db.Users
      .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

    if (user is null)
      return Result<Response>.Fail("User does not exists.");

    // TODO: Make a separate email update with account or email verification
    user.Email = request.Email;
    user.FirstName = request.FirstName;
    user.LastName = request.LastName;

    await _db.SaveChangesAsync(cancellationToken);

    var response = new Response(
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName,
        user.Role,
        user.Status,
        user.CompanyId,
        user.LastLoginAt,
        user.CreatedAt,
        user.UpdatedAt
    );

    return Result<Response>.Ok(response);
  }
}
