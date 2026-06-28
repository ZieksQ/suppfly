namespace Api.IntegrationTests.Shared.Utils;

public static class CookieHelper
{
  public static string GetCookieValue(HttpResponseMessage response, string cookieName)
  {
    response.Headers.TryGetValues("Set-Cookie", out var cookies);

    return cookies!
      .First(c => c.StartsWith($"{cookieName}="))
      .Split(';')[0]
      .Split('=')[1];
  }
}
