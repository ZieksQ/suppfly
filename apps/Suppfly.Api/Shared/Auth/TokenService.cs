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
  public string GenerateAccessToken(Guid userId, GlobalRole? role)
  {
    var jwtSettings = _config.GetSection("Jwt");
    var key = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

    var claims = new List<Claim>
    {
      new (ClaimTypes.NameIdentifier, userId.ToString()),
      // new Claim("account_status", status.ToString()),
      new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    if (role != null)
    {
      claims.Add(new Claim(ClaimTypes.Role, role.ToString()!));
    }

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
