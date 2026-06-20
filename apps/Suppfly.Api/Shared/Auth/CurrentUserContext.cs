using System.Security.Claims;
using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.Auth;

public class CurrentUserContext : ICurrentUserContext
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }
  public Guid UserId
  {
    get
    {
      var claim = _httpContextAccessor.HttpContext?.User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

      return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
  }

  public UserRole UserRole
  {
    get
    {
      var claim = _httpContextAccessor.HttpContext?.User
        .FindFirst(ClaimTypes.Role)?.Value;

      return Enum.TryParse<UserRole>(claim, out var role)
        ? role
        : throw new InvalidOperationException("Role claim is missing or invalid.");
    }
  }

  // public UserStatus Status
  // {
  //   get
  //   {
  //     var claim = _httpContextAccessor.HttpContext?.User
  //       .FindFirst("user_status")?.Value;
  //
  //     return Enum.TryParse<UserStatus>(claim, out var status)
  //       ? status
  //       : UserStatus.Disabled;
  //   }
  // }

  public bool IsAuthenticated =>
    _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}
