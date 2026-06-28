using Testcontainers.PostgreSql;

namespace Api.IntegrationTests.Infrastructure;

public class PostgreSqlContainer : IAsyncLifetime
{
  public Testcontainers.PostgreSql.PostgreSqlContainer Container { get; }

  public PostgreSqlContainer()
  {
    Container = new PostgreSqlBuilder("postgres:18.3-alpine3.23")
      .WithDatabase("testdb")
      .WithUsername("postgres")
      .WithPassword("postgres")
      .Build();
  }

  public string ConnectionString()
    => Container.GetConnectionString();

  public Task DisposeAsync()
    => Container.DisposeAsync().AsTask();

  public Task InitializeAsync()
    => Container.StartAsync();
}
