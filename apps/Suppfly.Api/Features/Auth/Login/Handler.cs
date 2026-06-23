using System.Security.Cryptography;
using System.Text;
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
  private readonly IConfiguration _config;

  public Handler(
      AppDbContext context,
      ITokenService tokenService,
      IConfiguration config)
  {
    _context = context;
    _tokenService = tokenService;
    _config = config;
  }

  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var user = await _context.Users
      .Include(u => u.CompanyUsers)
        .ThenInclude(cu => cu.Company)
      .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

    if (user is null)
      return Result<Response>.Fail("Incorrect email or password.");

    if (!user.IsEmailVerified)
      return Result<Response>.Fail("User and Company application is in progress.");

    bool hasCompanyActive = user.CompanyUsers
      .Any(cu => cu.Company.Status == CompanyStatus.Active);

    if (!hasCompanyActive && user.GlobalRole is null)
      return Result<Response>.Fail("User and Company application is in progress.");

    bool passwordIsValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

    if (!passwordIsValid)
      return Result<Response>.Fail("Incorrect email or password.");

    // Generate Access and Refresh Token
    string accessToken = _tokenService.GenerateAccessToken(
        user.Id,
        user.GlobalRole);

    string refreshToken = _tokenService.GenerateRefreshToken();

    // NOTE: Its advisable to use SHA256 in hashing 
    var tokenHash = SHA256.HashData(
        Encoding.UTF8.GetBytes(refreshToken));

    var tokenHashString = Convert.ToHexString(tokenHash);

    var jwtOptions = _config.GetSection("Jwt");

    var newRefreshToken = new RefreshToken
    {
      UserId = user.Id,
      TokenHash = tokenHashString,
      ExpiresAt = DateTime.UtcNow.AddDays(
        int.Parse(jwtOptions["RefreshTokenExpiryDays"]!))
    };

    _context.RefreshTokens.Add(newRefreshToken);
    await _context.SaveChangesAsync(cancellationToken);

    // var result = new UserDto(
    //     FirstName: user.FirstName,
    //     LastName: user.LastName,
    //     Email: user.Email,
    //     GlobalRole: user.GlobalRole.ToString() ?? null,
    //     Companies: [.. user.CompanyUsers.Select(cu => new Companies(
    //         cu.CompanyId,
    //         CompanyName: cu.Company.Name,
    //         Role: cu.Role.ToString(),
    //         JoinedAt: cu.JoinedAt))]);

    return Result<Response>.Ok(new Response(
          accessToken,
          refreshToken));
  }
}
