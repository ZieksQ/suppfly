using System.Net;
using Api.IntegrationTests.Shared;
using Api.IntegrationTests.Shared.Utils;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Suppfly.Api.Infrastructure.Persistence;

namespace Api.IntegrationTests.Features.Auth;

[Collection("IntegrationTests")]
public class RefreshTokenTests(IntegrationTestFixture fixture) : IntegrationTestBase(fixture)
{

  [Fact]
  public async Task Refresh_Token_Must_Be_Valid()
  {
    await LoginAsAdminAsync();

    var response = await Client.PostAsync("/api/v1/auth/refresh", null);

    response.StatusCode.Should()
      .Be(HttpStatusCode.OK);
  }

  [Fact]
  public async Task AccessToken_Must_Be_Rotating()
  {
    var login = await LoginAsAdminAsync();
    var firstToken = CookieHelper.GetCookieValue(login, "access_token");

    var refresh = await Client.PostAsync("/api/v1/auth/refresh", null);
    var secondToken = CookieHelper.GetCookieValue(refresh, "access_token");

    secondToken.Should().NotBe(firstToken);
  }

  [Fact]
  public async Task Refresh_Token_Invalid_When_Expired()
  {
    var login = await LoginAsAdminAsync();

    var db = Factory.Services.GetRequiredService<AppDbContext>();

    var userToken = await db.RefreshTokens
      .OrderByDescending(x => x.CreatedAt)
      .FirstAsync();

    userToken.Should().NotBeNull();
    userToken.ExpiresAt = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(1));
    await db.SaveChangesAsync();

    var refresh = await Client.PostAsync("/api/v1/auth/refresh", null);

    refresh.StatusCode.Should()
      .Be(HttpStatusCode.Forbidden);
  }
}
