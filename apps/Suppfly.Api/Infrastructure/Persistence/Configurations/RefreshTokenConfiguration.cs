using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suppfly.Api.Domain;

namespace Suppfly.Api.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
  public void Configure(EntityTypeBuilder<RefreshToken> builder)
  {
    builder.ToTable("refresh_tokens");

    builder.HasOne(r => r.User)
      .WithMany(u => u.RefreshTokens)
      .HasForeignKey(r => r.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
