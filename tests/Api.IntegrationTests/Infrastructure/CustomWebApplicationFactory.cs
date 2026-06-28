using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Suppfly.Api.Infrastructure.Persistence;

namespace Api.IntegrationTests.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  private readonly string _pgsqlConnectionString;
  private readonly string _redisConnectionString;

  public CustomWebApplicationFactory(
      string pgsqlConnectionString,
      string redisConnectionString)
  {
    _pgsqlConnectionString = pgsqlConnectionString;
    _redisConnectionString = redisConnectionString;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.UseEnvironment("Testing");

    builder.ConfigureServices(services =>
    {
      // Remove existing DbContext registration
      var descriptor = services
        .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

      if (descriptor is not null)
        services.Remove(descriptor);

      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseNpgsql(_pgsqlConnectionString);
      });

      // Redis desciptor
      var redisDescriptor = services
      .SingleOrDefault(d => d.ServiceType == typeof(IConnectionMultiplexer));

      if (redisDescriptor is not null)
        services.Remove(redisDescriptor);

      services.AddSingleton<IConnectionMultiplexer>(_ =>
          ConnectionMultiplexer.Connect(_redisConnectionString));

      var sp = services.BuildServiceProvider();

      using var scope = sp.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

      db.Database.Migrate();
    });
  }
}
