using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.RegisterB2BAccount;

public class Handler : IRequestHandler<Command, Result<Guid>>
{
  private readonly AppDbContext _db;

  public Handler(AppDbContext db)
  {
    _db = db;
  }

  public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
  {
    var userExists = await _db.Users
      .AnyAsync(u => u.Email == request.Email, cancellationToken);

    if (userExists)
      return Result<Guid>.Fail("An account with this email has already exists.");

    var companyExists = await _db.Companies
      .AnyAsync(c => c.Name == request.CompanyName
          || string.Equals(c.Slug, request.CompanySlug.ToLowerInvariant()),
          cancellationToken);

    if (companyExists)
      return Result<Guid>.Fail("A company with this name or slug already exists.");

    // NOTE: This Feature Creates a new transaction
    // it will create company, user and approval request
    // if something went wrong between those transactions it will rollback
    // and send an error instead of storing the record.
    await using var transaction =
      await _db.Database.BeginTransactionAsync(cancellationToken);

    try
    {
      var company = Company.Create(
          request.CompanyName,
          request.CompanySlug,
          request.CompanyType,
          request.TaxId,
          request.CompanyTier
      );

      _db.Companies.Add(company);

      await _db.SaveChangesAsync(cancellationToken);

      string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

      var user = User.Create(
          company.Id,
          request.Email,
          passwordHash,
          request.FirstName,
          request.LastName,
          UserRole.CompanyOwner
      );

      _db.Users.Add(user);
      await _db.SaveChangesAsync(cancellationToken);

      var approvalRequest = CompanyApprovalRequest.Create(
          company.Id,
          user.Id
      );

      _db.CompanyApprovalRequests.Add(approvalRequest);
      await _db.SaveChangesAsync(cancellationToken);

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
