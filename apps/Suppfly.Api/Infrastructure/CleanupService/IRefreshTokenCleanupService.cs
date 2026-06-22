namespace Suppfly.Api.Infrastructure.CleanupService;

public interface IRefreshTokenCleanupService
{
  Task<int> CleanupExpiredRefreshTokensAsync(
      CancellationToken cancellationToken = default);
}
