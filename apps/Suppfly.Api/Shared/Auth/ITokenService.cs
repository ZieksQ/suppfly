using System.Security.Claims;
using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.Auth;

public interface ITokenService
{
  string GenerateAccessToken(Guid userId, UserRole role, UserStatus status);
  string GenerateRefreshToken();
  ClaimsPrincipal? ValidateRefreshToken(string token);
}
