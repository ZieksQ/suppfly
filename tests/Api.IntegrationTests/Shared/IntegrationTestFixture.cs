using Api.IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Suppfly.Api.Infrastructure.SeederService;

namespace Api.IntegrationTests.Shared;

public class IntegrationTestFixture : IAsyncLifetime
{
  private readonly PostgreSqlContainer _postgres = new();
  private readonly RedisContainer _redis = new();

  public CustomWebApplicationFactory Factory { get; private set; } = default!;

  public HttpClient CreateClient()
  {
    return Factory.CreateClient(new WebApplicationFactoryClientOptions
    {
      AllowAutoRedirect = false,
      HandleCookies = true
    });
  }

  public async Task DisposeAsync()
  {
    Factory.Dispose();

    await _postgres.DisposeAsync();
    await _redis.DisposeAsync();
  }

  public async Task InitializeAsync()
  {
    await _postgres.InitializeAsync();
    await _redis.InitializeAsync();

    Factory = new CustomWebApplicationFactory(
        _postgres.ConnectionString(),
        _redis.ConnectionString());

    using var scope = Factory.Services.CreateScope();

    var seeder = scope.ServiceProvider
      .GetRequiredService<IDataSeeder>();

    await seeder.SeedAsync();
  }
}
