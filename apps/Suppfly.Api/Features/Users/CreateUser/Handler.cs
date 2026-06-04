using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Features.Users.CreateUser;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    bool emailExists = await _db.Users
      .AnyAsync(u => u.Email == request.Email, cancellationToken);

    if (emailExists)
      return Result<Response>.Fail($"Email '{request.Email}' is already in use.");

    bool companyExists = await _db.Companies
      .AnyAsync(c => c.Id == request.CompanyId, cancellationToken);

    if (!companyExists)
      return Result<Response>.Fail("Company not found.");

    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var user = User.Create(
        request.CompanyId,
        request.Email,
        passwordHash,
        request.FirstName,
        request.LastName,
        request.Role
    );

    _db.Users.Add(user);
    await _db.SaveChangesAsync(cancellationToken);

    var response = new Response(
      user.Id,
      $"{user.FirstName} {user.LastName}",
      user.Email,
      user.Role,
      user.Status,
      user.CreatedAt
    );

    return Result<Response>.Ok(response);
  }
}
