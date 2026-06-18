using System.Security.Claims;
using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.Auth;

public interface ITokenService
{
  string GenerateAccessToken(Guid userId, GlobalRole? globalRole);
  string GenerateRefreshToken();
  ClaimsPrincipal? ValidateRefreshToken(string token);
}
