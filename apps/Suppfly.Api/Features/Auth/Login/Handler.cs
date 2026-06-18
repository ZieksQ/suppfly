using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Entities;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.Login;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _context;
  private readonly ITokenService _tokenService;

  public Handler(AppDbContext context, ITokenService tokenService)
  {
    _context = context;
    _tokenService = tokenService;
  }

  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var user = await _context.Users
      .Include(u => u.CompanyUsers)
        .ThenInclude(cu => cu.Company)
      .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

    if (user is null)
      return Result<Response>.Fail("Incorrect email or password.");

    if (!user.EmailVerified)
      return Result<Response>.Fail("User and Company application is in progress.");

    bool hasCompanyActive = user.CompanyUsers
      .Any(cu => cu.Company.Status == CompanyStatus.Active);

    if (!hasCompanyActive)
      return Result<Response>.Fail("User and Company application is in progress.");

    bool passwordIsValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

    if (!passwordIsValid)
      return Result<Response>.Fail("Incorrect email or password.");

    string accessToken = _tokenService.GenerateAccessToken(
        user.Id,
        user.GlobalRole);

    string refreshToken = _tokenService.GenerateRefreshToken();
    string tokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);

    var newRefreshToken = new RefreshToken
    {
      UserId = user.Id,
      TokenHash = tokenHash,
      ExpiresAt = DateTime.UtcNow.AddDays(7)
    };

    _context.RefreshTokens.Add(newRefreshToken);
    await _context.SaveChangesAsync(cancellationToken);

    var result = new Response(
        FirstName: user.FirstName,
        LastName: user.LastName,
        Email: user.Email,
        GlobalRole: user.GlobalRole.ToString() ?? null,
        Companies: [.. user.CompanyUsers.Select(cu => new Companies(
            cu.CompanyId,
            CompanyName: cu.Company.Name,
            Role: cu.Role.ToString(),
            JoinedAt: cu.JoinedAt))]);

    return Result<Response>.Ok(result);
  }
}
