using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Entities;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.RegisterTenant;

public class Handler : IRequestHandler<Command, Result<Guid>>
{
  private readonly AppDbContext _context;

  public Handler(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
  {
    var userExists = await _context.Users
      .AnyAsync(u => u.Email == request.Email, cancellationToken);

    if (userExists)
      return Result<Guid>.Fail("Registration Failed Email or Company Already Exists.");

    var companyExists = await _context.Companies
      .AnyAsync(u => u.Name == request.CompanyName, cancellationToken);

    if (companyExists)
      return Result<Guid>.Fail("Registration Failed Email or Company Already Exists.");

    await using var transaction =
      await _context.Database.BeginTransactionAsync(cancellationToken);

    try
    {
      string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

      var user = User.Create(
          email: request.Email,
          passwordHash: passwordHash,
          firstName: request.FirstName,
          lastName: request.LastName);

      _context.Users.Add(user);
      await _context.SaveChangesAsync(cancellationToken);

      var company = Company.Create(request.CompanyName);

      _context.Companies.Add(company);
      await _context.SaveChangesAsync(cancellationToken);

      var companyUser = CompanyUser.Create(
          companyId: company.Id,
          userId: user.Id,
          role: UserRole.org_admin);

      _context.CompanyUsers.Add(companyUser);
      await _context.SaveChangesAsync(cancellationToken);

      await transaction.CommitAsync(cancellationToken);

      return Result<Guid>.Ok(user.Id);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync(cancellationToken);
      return Result<Guid>.Fail(ex.Message);
    }
  }
}
