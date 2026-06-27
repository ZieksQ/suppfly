using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Suppfly.Api.Infrastructure.Persistence;

namespace Api.IntegrationTests.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  private readonly string _connectionString;

  public CustomWebApplicationFactory(string connectionString)
  {
    _connectionString = connectionString;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      builder.UseEnvironment("Testing");
      // Remove existing DbContext registration
      var descriptor = services
        .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

      if (descriptor is not null)
        services.Remove(descriptor);

      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseNpgsql(_connectionString);
      });

      var sp = services.BuildServiceProvider();

      using var scope = sp.CreateScope();

      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

      db.Database.Migrate();
    });
  }
}
