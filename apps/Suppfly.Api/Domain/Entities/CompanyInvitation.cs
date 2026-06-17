using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain.Entities;

public class CompanyInvitation : BaseEntity
{
  public Guid CompanyId { get; set; }
  public Company Company { get; set; } = null!;

  public string Email { get; set; } = string.Empty;
  public UserRole Role { get; set; }
  public string Token { get; set; } = string.Empty;
  public DateTime ExpiredAt { get; set; }
  public DateTime? AcceptedAt { get; set; }

  public Guid InvitedByUserId { get; set; }
  public User InvitedByUser { get; set; } = null!;
}
