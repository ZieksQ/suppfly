using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain;

// NOTE: Add invited user by in the future or create new table for invited users
public class User : BaseEntity
{
  public Guid CompanyId { get; set; }
  public Company? Company { get; set; }

  public string Email { get; set; } = string.Empty;
  public string? PasswordHash { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public UserRole Role { get; set; }
  public UserStatus Status { get; set; }
  public DateTime? LastLoginAt { get; set; }

  public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

  private User() { }

  public static User Create(
          Guid companyId,
          string email,
          string passwordHash,
          string firstName,
          string lastName,
          UserRole role)
  {
    return new User
    {
      CompanyId = companyId,
      Email = email,
      PasswordHash = passwordHash,
      FirstName = firstName,
      LastName = lastName,
      Role = role,
      Status = UserStatus.Disabled
    };
  }
}
