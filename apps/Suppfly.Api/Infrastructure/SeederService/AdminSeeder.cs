using Microsoft.EntityFrameworkCore;
using Suppfly.Api.Domain.Entities;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Infrastructure.Persistence;

namespace Suppfly.Api.Infrastructure.SeederService;

public class AdminSeeder : IDataSeeder
{
  private readonly AppDbContext _context;

  public AdminSeeder(AppDbContext context)
  {
    _context = context;
  }

  public async Task SeedAsync(CancellationToken cancellationToken = default)
  {
    string email = "admin@email.com";

    var exists = await _context.Users
      .AnyAsync(x => x.Email == email, cancellationToken);

    string passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123");

    if (exists)
      return;

    var admin = User.CreateAdministrator(
        email: email,
        passwordHash: passwordHash,
        firstName: "admin",
        lastName: "zieks",
        globalRole: GlobalRole.super_admin);

    _context.Users.Add(admin);

    await _context.SaveChangesAsync(cancellationToken);
  }
}
