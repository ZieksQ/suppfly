using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain;

public class Company : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public CompanyType Type { get; set; }
    public string? TaxId { get; set; }
    public CompanyStatus Status { get; set; }
    public CompanyTier Tier { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Guid? ApprovedByUserId { get; set; }

    public User? ApprovedByUser { get; set; }
    public ICollection<User> Users { get; set; } = [];

    private Company() { }

    public static Company Create(
            string name,
            string slug,
            CompanyType type,
            string? taxId = null,
            CompanyTier tier = CompanyTier.Standard
            )
    {
        return new Company
        {
            Name = name,
            Slug = slug,
            Type = type,
            TaxId = taxId,
            Status = CompanyStatus.Pending,
            Tier = tier
        };
    }
}
