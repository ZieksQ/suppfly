using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Users.ChangePassword;

public class Handler : IRequestHandler<Command, Result>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }
  public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
  {
    var user = await _db.Users
      .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

    if (user is null)
      return Result.Fail("User does not exists.");

    bool isValid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);

    if (!isValid)
      return Result.Fail("Current password did not match.");

    string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

    user.PasswordHash = newPasswordHash;

    await _db.SaveChangesAsync(cancellationToken);

    return Result.Ok();
  }
}
