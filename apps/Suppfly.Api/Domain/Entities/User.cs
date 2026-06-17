using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain.Entities;

public class User : BaseEntity
{
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public GlobalRole? GlobalRole { get; set; }
  public bool EmailVerified { get; set; }
  public bool IsActive { get; set; }

  public ICollection<CompanyUser> CompanyUsers { get; set; } = [];
  public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

  private User() { }

  public static User Create(
      string email,
      string passwordHash,
      string firstName,
      string lastName)
  {
    return new User
    {
      Email = email,
      PasswordHash = passwordHash,
      FirstName = firstName,
      LastName = lastName,
      GlobalRole = null,
      EmailVerified = false,
      IsActive = false
    };
  }

  public static User CreateAdministrator(
    string email,
    string passwordHash,
    string firstName,
    string lastName,
    GlobalRole globalRole)
  {
    return new User
    {
      Email = email,
      PasswordHash = passwordHash,
      FirstName = firstName,
      LastName = lastName,
      GlobalRole = globalRole,
      EmailVerified = true,
      IsActive = true
    };
  }

  public void Accept()
  {
    EmailVerified = true;
    IsActive = true;
  }
}
