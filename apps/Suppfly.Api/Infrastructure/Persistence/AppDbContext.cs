using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Entities;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<User> Users => Set<User>();
  public DbSet<Company> Companies => Set<Company>();
  public DbSet<CompanyUser> CompanyUsers => Set<CompanyUser>();
  public DbSet<CompanyInvitation> CompanyInvitations => Set<CompanyInvitation>();
  public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
    {
      if (entry.State == EntityState.Added)
      {
        entry.Entity.UpdatedAt = DateTime.UtcNow;
      }
      else if (entry.State == EntityState.Modified)
      {
        entry.Entity.UpdatedAt = DateTime.UtcNow;
      }
    }
    return base.SaveChangesAsync(cancellationToken);
  }
}

