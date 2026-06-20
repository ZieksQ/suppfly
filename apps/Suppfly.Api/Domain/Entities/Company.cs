using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain.Entities;

public class Company : BaseEntity
{
  public string Name { get; set; } = string.Empty;
  public CompanyStatus Status { get; set; }
  public Guid CreatedByUserId { get; set; }
  public DateTime? ApprovedAt { get; set; }
  public Guid? ApprovedByUserId { get; set; }
  public DateTime? RejectedAt { get; set; }
  public Guid? RejectedByUserId { get; set; }
  public string? RejectionReason { get; set; }

  public User CreatedByUser { get; set; } = null!;
  public User? ApprovedByUser { get; set; }
  public User? RejectedByUser { get; set; }

  public ICollection<CompanyUser> CompanyUsers { get; set; } = [];

  private Company() { }

  public static Company Create(
    string name,
    Guid createdByUserId)
  {
    return new Company
    {
      Name = name,
      CreatedByUserId = createdByUserId,
      Status = CompanyStatus.Pending_Approval
    };
  }

  public void Approved(Guid approvedByUserId)
  {
    Status = CompanyStatus.Active;
    ApprovedAt = DateTime.UtcNow;
    ApprovedByUserId = approvedByUserId;
  }

  public void Rejected(Guid rejectedByUserId, string? rejectionReason)
  {
    Status = CompanyStatus.Rejected;
    RejectedAt = DateTime.UtcNow;
    RejectedByUserId = rejectedByUserId;
    RejectionReason = rejectionReason;
  }

  public void Suspended()
  {
    Status = CompanyStatus.Suspended;
  }

  public void Disabled()
  {
    Status = CompanyStatus.Disabled;
  }
}
