using System.Net.Http.Json;
using Api.IntegrationTests.Infrastructure;

namespace Api.IntegrationTests.Shared.Utils;

public class IntegrationTestBase
{
  protected readonly HttpClient Client;
  protected readonly CustomWebApplicationFactory Factory;

  protected IntegrationTestBase(IntegrationTestFixture fixture)
  {
    Factory = fixture.Factory;
    Client = fixture.CreateClient();
  }

  protected async Task<HttpResponseMessage> LoginAsAdminAsync()
  {
    var request = new
    {
      Email = "admin@email.com",
      Password = "Admin123"
    };

    var response = await Client.PostAsJsonAsync(
        "/api/v1/auth/login",
        request);

    response.EnsureSuccessStatusCode();

    return response;

    // if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
    // {
    //   foreach (var cookie in cookies)
    //   {
    //     Console.WriteLine(cookie);
    //   }
    // }
    // else
    // {
    //   Console.WriteLine("No Set-Cookie header returned.");
    // }
  }
}
