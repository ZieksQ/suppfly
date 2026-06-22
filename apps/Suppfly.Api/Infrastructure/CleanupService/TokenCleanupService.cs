using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Infrastructure.Persistence;

namespace Suppfly.Api.Infrastructure.CleanupService;

public class TokenCleanupService : BackgroundService
{
  private readonly IServiceProvider _service;
  private readonly ILogger<TokenCleanupService> _logger;
  private readonly TimeSpan _runInterval = TimeSpan.FromHours(24); // interval: 24 hours

  public TokenCleanupService(
      IServiceProvider service,
      ILogger<TokenCleanupService> logger)
  {
    _service = service;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      _logger.LogInformation("Refresh token cleanup job started at: {time}", DateTimeOffset.UtcNow);

      try
      {
        using var scope = _service.CreateScope();

        var cleanupService = scope.ServiceProvider
          .GetRequiredService<IRefreshTokenCleanupService>();

        // DELETE expired or revoked refresh tokens
        var count = await cleanupService.CleanupExpiredRefreshTokensAsync(stoppingToken);

        _logger.LogInformation(
            "Deleted {count} expired/revoked refresh tokens.", count);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error has occured while cleaning up refresh tokens.");
      }

      await Task.Delay(_runInterval, stoppingToken);
    }
  }
}
