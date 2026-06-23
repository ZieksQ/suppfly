namespace Suppfly.Api.Infrastructure.SeederService;

public interface IDataSeeder
{
  Task SeedAsync(CancellationToken cancellationToken = default);
}
