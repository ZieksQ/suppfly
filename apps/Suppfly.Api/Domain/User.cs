using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain;

public class User : BaseEntity
{
    public Guid CompanyId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public Company Company { get; set; } = null!;

    private User() { }

    public static User Create(
            Guid companyId,
            string email,
            string passwordHash,
            string lastName,
            string firstName,
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
            Status = UserStatus.Inactive
        };
    }
}
