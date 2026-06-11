using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.RegisterUser;

public class Handler : IRequestHandler<Command, Result<Guid>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }
  public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
  {
    var exists = await _db.Users
      .AnyAsync(u => u.Email == request.Email.ToLowerInvariant(), cancellationToken);

    if (exists)
      return Result<Guid>.Fail("An account with this email already exists.");

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var user = User.Create(
        request.CompanyId,
        request.Email.ToLowerInvariant(),
        passwordHash,
        request.FirstName,
        request.LastName,
        request.Role
    );

    _db.Users.Add(user);
    await _db.SaveChangesAsync(cancellationToken);

    return Result<Guid>.Ok(user.Id);
  }
}
