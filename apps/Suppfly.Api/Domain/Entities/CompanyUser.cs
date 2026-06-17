using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Domain.Entities;

public class CompanyUser
{
  public Guid CompanyId { get; set; }
  public Company Company { get; set; } = null!;

  public Guid UserId { get; set; }
  public User User { get; set; } = null!;

  public UserRole Role { get; set; }
  public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

  private CompanyUser() { }

  public static CompanyUser Create(
    Guid companyId,
    Guid userId,
    UserRole role)
  {
    return new CompanyUser
    {
      CompanyId = companyId,
      UserId = userId,
      Role = role
    };
  }
}
