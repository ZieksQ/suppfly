using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.Shared;
using Api.IntegrationTests.Shared.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Suppfly.Api.Infrastructure.Persistence;
using Suppfly.Api.Shared.Auth;

namespace Api.IntegrationTests.Features.Auth;

[Collection("IntegrationTests")]
public class RefreshTokenTests(IntegrationTestFixture fixture)
{
  private readonly HttpClient _client = fixture.Client;

  [Fact]
  public async Task Refresh_Token_Must_Be_Valid()
  {
    var payload = new
    {
      Email = "admin@email.com",
      Password = "Admin123"
    };

    var login = await _client.PostAsJsonAsync(
        "/api/v1/auth/login",
        payload);

    login.StatusCode.Should()
      .Be(HttpStatusCode.OK);

    login.Headers.TryGetValues("Set-Cookie", out var cookies);

    cookies.Should().Contain(c => c.Contains("refresh_token="));

    var response = await _client.SendAsync(new HttpRequestMessage(
          HttpMethod.Post,
          "/api/v1/auth/refresh"));

    response.StatusCode.Should()
      .Be(HttpStatusCode.OK);
  }
}
