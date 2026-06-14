using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Domain;

public class CompanyApprovalRequest : BaseEntity
{
  public Guid CompanyId { get; set; }
  public Company Company { get; set; } = null!;

  public Guid RequestedByUserId { get; set; }
  public User RequestedByUser { get; set; } = null!;

  public ApprovalStatus Status { get; set; }
  public string? Notes { get; set; } = null;

  public Guid? ReviewedByUserId { get; set; } = null;
  public User? ReviewedByUser { get; set; } = null;

  public DateTime? ReviewedAt { get; set; } = null;

  private CompanyApprovalRequest() { }

  public static CompanyApprovalRequest Create(
          Guid companyId,
          Guid requestedByUserId
          )
  {
    return new CompanyApprovalRequest
    {
      CompanyId = companyId,
      RequestedByUserId = requestedByUserId,
      Status = ApprovalStatus.Pending
    };
  }

  public void Reject(Guid reviewedByUserId, string? notes = null)
  {
    Status = ApprovalStatus.Rejected;
    ReviewedByUserId = reviewedByUserId;
    ReviewedAt = DateTime.UtcNow;
    Notes = notes;
  }

  public void Approved(Guid reviewedByUserId, string? notes = null)
  {
    Status = ApprovalStatus.Approved;
    ReviewedByUserId = reviewedByUserId;
    ReviewedAt = DateTime.UtcNow;
    Notes = notes;
  }

  public void MoreInfoNeeded(Guid reviewedByUserId, string? notes = null)
  {
    Status = ApprovalStatus.MoreInfoNeeded;
    ReviewedByUserId = reviewedByUserId;
    ReviewedAt = DateTime.UtcNow;
    Notes = notes;
  }
}
