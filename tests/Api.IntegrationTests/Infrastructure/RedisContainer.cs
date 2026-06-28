using Testcontainers.Redis;

namespace Api.IntegrationTests.Infrastructure;

public class RedisContainer : IAsyncLifetime
{
  private readonly Testcontainers.Redis.RedisContainer _container;

  public RedisContainer()
  {
    _container = new RedisBuilder("redis:alpine")
      .WithPortBinding(6379, true)
      .Build();
  }

  public string ConnectionString()
    => _container.GetConnectionString();

  public Task DisposeAsync()
    => _container.DisposeAsync().AsTask();

  public Task InitializeAsync()
    => _container.StartAsync();
}
