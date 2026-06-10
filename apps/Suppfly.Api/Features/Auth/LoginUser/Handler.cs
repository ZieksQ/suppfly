using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.LoginUser;

public class Handler : IRequestHandler<Command, Result<Response>>
{
  private readonly AppDbContext _db;
  private readonly ITokenService _tokenService;

  public Handler(AppDbContext db, ITokenService tokenService)
  {
    _db = db;
    _tokenService = tokenService;
  }
  public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
  {
    var user = await _db.Users
      .FirstOrDefaultAsync(u => u.Email == request.Email.ToLowerInvariant(), cancellationToken);

    // NOTE: do not say "email not found". 
    // this leaks the information to attackers
    if (user is null)
      return Result<Response>.Fail("Invalid email or password.");

    // verify password
    var passwordIsValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

    if (!passwordIsValid)
      return Result<Response>.Fail("Invalid email or password.");

    // check user status
    if (user.Status != UserStatus.Active)
      return Result<Response>.Fail("Your account approval is pending.");

    // generate tokens
    var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Role, user.Status);
    var refreshToken = _tokenService.GenerateRefreshToken();

    // store the refresh token
    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

    await _db.SaveChangesAsync(cancellationToken);

    return Result<Response>.Ok(new Response(
          AccessToken: accessToken,
          RefreshToken: refreshToken,
          UserId: user.Id,
          Role: user.Role,
          CompanyId: user.CompanyId
          ));
  }
}
