using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;

namespace Suppfly.Api.Infrastructure.CleanupService;

public class RefreshTokenCleanupService : IRefreshTokenCleanupService
{
  private readonly AppDbContext _context;

  public RefreshTokenCleanupService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<int> CleanupExpiredRefreshTokensAsync(CancellationToken cancellationToken = default)
  {
    var expiredTokens = await _context.RefreshTokens
      .Where(rt => rt.ExpiresAt <= DateTime.UtcNow || rt.RevokedAt != null)
      .ToListAsync(cancellationToken);

    if (expiredTokens.Count == 0)
      return 0;

    _context.RefreshTokens.RemoveRange(expiredTokens);
    await _context.SaveChangesAsync(cancellationToken);

    return expiredTokens.Count;
  }
}
