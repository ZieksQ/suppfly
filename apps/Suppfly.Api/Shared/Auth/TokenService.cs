using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.Auth;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;

  public TokenService(IConfiguration config)
  {
    _config = config;
  }
  public string GenerateAccessToken(Guid userId, UserRole role, UserStatus status)
  {
    var jwtSettings = _config.GetSection("Jwt");
    var key = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
      new Claim(ClaimTypes.Role, role.ToString()),
      new Claim("account_status", status.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var token = new JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(
          int.Parse(jwtSettings["AccessTokenExpiryMinutes"]!)),
        signingCredentials: new SigningCredentials(
          key, SecurityAlgorithms.HmacSha256)
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public string GenerateRefreshToken()
  {
    var bytes = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(bytes);
    return Convert.ToBase64String(bytes);
  }

  public ClaimsPrincipal? ValidateRefreshToken(string token)
  {
    return null;
  }
}
