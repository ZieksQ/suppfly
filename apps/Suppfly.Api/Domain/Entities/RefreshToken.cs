using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain.Entities;

public class RefreshToken : BaseEntity
{
  public Guid UserId { get; set; }
  public User User { get; set; } = null!;

  public string TokenHash { get; set; } = string.Empty;
  public DateTime ExpiresAt { get; set; }
  public DateTime? RevokedAt { get; set; }

  public bool Revoked => RevokedAt != null;
  public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
  public bool IsActive => !Revoked && !IsExpired;
}
