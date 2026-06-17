using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppfly.Api.Domain.Entities;

namespace Suppfly.Api.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");

    builder.HasKey(u => u.Id);

    builder.Property(u => u.Email)
      .IsRequired()
      .HasMaxLength(255);

    builder.HasIndex(u => u.Email)
      .IsUnique();

    builder.Property(u => u.PasswordHash)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.FirstName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.LastName)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.GlobalRole)
      .HasConversion<string>()
      .HasMaxLength(50);
  }
}
