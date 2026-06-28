using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Api.IntegrationTests.Features.Auth;

[Collection("IntegrationTests")]
public class RefreshTokenTests(IntegrationTestFixture fixture)
{
  private readonly IntegrationTestFixture _fixture = fixture;

  [Fact]
  public async Task Refresh_Token_Must_Be_Valid()
  {
    var client = _fixture.CreateClient();

    var payload = new
    {
      Email = "admin@email.com",
      Password = "Admin123"
    };

    var login = await client.PostAsJsonAsync(
        "/api/v1/auth/login",
        payload);

    login.StatusCode.Should()
      .Be(HttpStatusCode.OK);

    login.Headers.TryGetValues("Set-Cookie", out var cookies);

    foreach (var cookie in cookies!)
    {
      Console.WriteLine(cookie);
    }

    cookies.Should().Contain(c => c.Contains("refresh_token="));

    var request = new HttpRequestMessage(
        HttpMethod.Post,
        "/api/v1/auth/refresh");

    Console.WriteLine(request.Headers);

    var response = await client.SendAsync(request);

    response.StatusCode.Should()
      .Be(HttpStatusCode.OK);
  }
}
