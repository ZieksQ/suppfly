using System.Security.Claims;
using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.Auth;

public interface ITokenService
{
  string GenerateAccessToken(Guid userId, GlobalRole? globalRole);
  string GenerateRefreshToken();
  // bool ValidateRefrershToken(string token, string tokenHash);
  // ClaimsPrincipal? ValidateRefreshToken(string token);
}
