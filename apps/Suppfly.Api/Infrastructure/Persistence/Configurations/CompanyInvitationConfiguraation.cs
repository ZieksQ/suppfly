using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppfly.Api.Domain.Entities;

namespace Suppfly.Api.Infrastructure.Persistence.Configurations;

public class CompanyInvitationConfiguraation : IEntityTypeConfiguration<CompanyInvitation>
{
  public void Configure(EntityTypeBuilder<CompanyInvitation> builder)
  {
    builder.ToTable("company_invitations");

    builder.HasKey(inv => inv.Id);

    builder.Property(inv => inv.Email)
      .IsRequired()
      .HasMaxLength(255);

    builder.HasIndex(inv => inv.Email)
      .IsUnique();

    builder.Property(inv => inv.Role)
      .IsRequired()
      .HasConversion<string>()
      .HasMaxLength(50);

    builder.Property(inv => inv.Token)
      .HasMaxLength(255);

    builder.HasOne(inv => inv.Company)
      .WithMany()
      .HasForeignKey(inv => inv.CompanyId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(inv => inv.InvitedByUser)
      .WithMany()
      .HasForeignKey(inv => inv.InvitedByUserId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
