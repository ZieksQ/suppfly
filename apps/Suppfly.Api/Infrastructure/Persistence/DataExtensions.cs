using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Suppfly.Api.Infrastructure.Persistence;

public static class DataExtensions
{
  public static void MigrateDb(this WebApplication app)
  {
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
  }

  public static void AddAppInfrastructure(this WebApplicationBuilder builder)
  {
    var postgresConnString = builder.Configuration.GetConnectionString("Postgres")
      ?? throw new InvalidOperationException("PostgreSQL Connection string does not exists.");

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(postgresConnString)
          .UseSnakeCaseNamingConvention()); // Added Snake Case from EfCore.NamingConvention package

    var redisConnString = builder.Configuration.GetConnectionString("Redis")
      ?? throw new InvalidOperationException("Redis Connection string does not exists.");

    builder.Services.AddSingleton<IConnectionMultiplexer>(
        ConnectionMultiplexer.Connect(redisConnString));
  }
}
