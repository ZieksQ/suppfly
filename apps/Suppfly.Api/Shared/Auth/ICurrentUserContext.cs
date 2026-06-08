using Suppfly.Api.Domain.Enums;

namespace Suppfly.Api.Shared.Auth;

public interface ICurrentUserContext
{
  Guid UserId { get; }
  UserRole UserRole { get; }
  UserStatus Status { get; }
  bool IsAuthenticated { get; }
}
