using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.Refresh;

public class Handler : IRequestHandler<Command, Result<string>>
{
  private readonly AppDbContext _context;
  private readonly ITokenService _tokenService;

  public Handler(
      AppDbContext context,
      ITokenService tokenService)
  {
    _context = context;
    _tokenService = tokenService;
  }

  public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
  {
    var incomingToken = Convert.ToHexString(
      SHA256.HashData(
        Encoding.UTF8.GetBytes(request.RefreshToken)));

    var refreshToken = await _context.RefreshTokens
      .Include(rt => rt.User)
      .FirstOrDefaultAsync(rt => rt.TokenHash == incomingToken, cancellationToken);

    if (refreshToken is null)
      return Result<string>.Fail("Refresh Token invalid unauthorize user.");

    if (!refreshToken.IsActive)
      return Result<string>.Fail("Refresh Token invalid unauthorize user.");

    string accessToken = _tokenService.GenerateAccessToken(
        refreshToken.User.Id,
        refreshToken.User.GlobalRole);

    return Result<string>.Ok(accessToken);
  }
}
