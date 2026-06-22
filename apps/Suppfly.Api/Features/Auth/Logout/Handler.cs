using MediatR;
using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;

namespace Suppfly.Api.Features.Auth.Logout;

public class Handler : IRequestHandler<Command>
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

  // NOTE: This feature will check for refreshtoken in the database and revoke it
  public async Task Handle(Command request, CancellationToken cancellationToken)
  {
    var tokenHash = _tokenService.HashRefreshToken(request.RefreshToken);

    var refreshToken = await _context.RefreshTokens
      .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);

    if (refreshToken is null)
      return;

    refreshToken.RevokedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync(cancellationToken);
  }
}
