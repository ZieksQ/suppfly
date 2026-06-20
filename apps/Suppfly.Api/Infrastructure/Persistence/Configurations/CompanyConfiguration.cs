using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppfly.Api.Domain.Entities;

namespace Suppfly.Api.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.ToTable("companies");

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Name)
      .IsRequired()
      .HasMaxLength(255);

    builder.Property(c => c.Status)
      .IsRequired()
      .HasConversion<string>()
      .HasMaxLength(50);

    builder.HasOne(c => c.CreatedByUser)
      .WithMany()
      .HasForeignKey(c => c.CreatedByUserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(c => c.ApprovedByUser)
      .WithMany()
      .HasForeignKey(c => c.ApprovedByUserId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(c => c.RejectedByUser)
      .WithMany()
      .HasForeignKey(c => c.RejectedByUserId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.Property(c => c.RejectionReason)
      .HasMaxLength(1000);
  }
}
