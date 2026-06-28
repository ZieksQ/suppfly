using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Suppfly.Api.Infrastructure.SeederService;

namespace Suppfly.Api.Infrastructure.Persistence;

public static class DataExtensions
{
  public static void MigrateDb(this WebApplication app)
  {
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
  }

  /// <summary>
  ///   Seed data from IDataSeeder services.
  /// </summary>
  public static async Task SeedData(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var seeders = scope.ServiceProvider
      .GetServices<IDataSeeder>();

    foreach (var seeder in seeders)
    {
      await seeder.SeedAsync();
    }
  }

  public static void AddAppInfrastructure(this WebApplicationBuilder builder)
  {
    var postgresConnString = builder.Configuration.GetConnectionString("Postgres")
      ?? throw new InvalidOperationException("PostgreSQL Connection string does not exists.");

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(postgresConnString)
          .UseSnakeCaseNamingConvention()); // Added Snake Case from EfCore.NamingConvention package

    builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    {
      var redisConnString = builder.Configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("Redis Connection string does not exists.");

      return ConnectionMultiplexer.Connect(redisConnString);
    });
  }
}
