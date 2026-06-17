using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppfly.Api.Domain.Entities;

namespace Suppfly.Api.Infrastructure.Persistence.Configurations;

public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
  public void Configure(EntityTypeBuilder<CompanyUser> builder)
  {
    builder.ToTable("company_users");

    builder.HasOne(cu => cu.Company)
      .WithMany(c => c.CompanyUsers)
      .HasForeignKey(cu => cu.CompanyId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(cu => cu.User)
      .WithMany(u => u.CompanyUsers)
      .HasForeignKey(cu => cu.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(cu => cu.Role)
      .IsRequired()
      .HasConversion<string>()
      .HasMaxLength(50);
  }
}
