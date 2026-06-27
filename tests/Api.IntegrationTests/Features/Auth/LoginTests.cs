using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.Shared;
using FluentAssertions;

namespace Api.IntegrationTests.Features.Auth;

[Collection("IntegrationTests")]
public class LoginTests
{
  private readonly HttpClient _client;

  public LoginTests(IntegrationTestFixture fixture)
  {
    _client = fixture.Client;
  }

  [Fact]
  public async Task Login_Should_Return_Unauthorized_When_Invalid()
  {
    var request = new
    {
      Email = "wrong@test.com",
      Password = "wrong"
    };

    var response = await _client.PostAsJsonAsync(
        "/api/v1/auth/login",
        request);

    response.StatusCode.Should()
      .Be(HttpStatusCode.Unauthorized);
  }

  [Fact]
  public async Task Login_Should_Be_Authenticated_When_Valid()
  {
    var request = new
    {
      Email = "admin@email.com",
      Password = "Admin123"
    };

    var response = await _client.PostAsJsonAsync(
        "/api/v1/auth/login",
        request);

    response.StatusCode.Should().Be(HttpStatusCode.OK);

    response.Headers.TryGetValues("Set-Cookie", out var cookies)
      .Should().BeTrue();

    cookies.Should().Contain(c =>
        c.Contains("access_token=") &&
        c.Contains("httponly"));
    cookies.Should().Contain(c =>
        c.Contains("refresh_token=") &&
        c.Contains("httponly"));
  }
}
