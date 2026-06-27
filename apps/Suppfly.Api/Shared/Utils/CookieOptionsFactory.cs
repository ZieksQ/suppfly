namespace Suppfly.Api.Shared.Utils;

/// <summary>
///   Default Cookie Options for Auth Tokens
/// </summary>
public sealed class CookieOptionsFactory
{
  /// <summary>
  ///   returns cookie options configured for access token  
  /// </summary>
  /// <param name="expiryMinutes">Expiration time in minutes. Defaults to 30.</param>
  /// <returns>A <see cref="CookieOptions" />object for the access token.</returns>
  public static CookieOptions AccessToken(IHostEnvironment env, int expiryMinutes = 30)
  {
    return new CookieOptions
    {
      HttpOnly = true,
      Secure = !env.IsEnvironment("Testing"),
      SameSite = SameSiteMode.Strict,
      Expires = DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)
    };
  }

  /// <summary>
  ///   returns cookie options configured for refresh token 
  /// </summary>
  /// <param name="expiryDays">Expiration time in minutes. Defaults to 7.</param>
  /// <returns>A <see cref="CookieOptions" />object for the refresh token.</returns>
  public static CookieOptions RefreshToken(IHostEnvironment env, int expiryDays = 7)
  {
    return new CookieOptions
    {
      HttpOnly = true,
      Secure = !env.IsEnvironment("Testing"),
      SameSite = SameSiteMode.Strict,
      Expires = DateTimeOffset.UtcNow.AddDays(expiryDays)
    };
  }
}
