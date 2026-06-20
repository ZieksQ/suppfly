using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppfly.Api.Domain.Entities;

namespace Suppfly.Api.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
  public void Configure(EntityTypeBuilder<RefreshToken> builder)
  {
    builder.ToTable("refresh_tokens");

    builder.HasKey(rt => rt.Id);

    builder.Property(rt => rt.TokenHash)
      .IsRequired()
      .HasMaxLength(100);

    builder.HasOne(rt => rt.User)
      .WithMany(u => u.RefreshTokens)
      .HasForeignKey(rt => rt.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
