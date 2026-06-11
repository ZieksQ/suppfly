using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.LoginUser;

public record Command(
  string Email,
  string Password
) : IRequest<Result<Response>>;

public record Response(
  string AccessToken,
  string RefreshToken,
  Guid UserId,
  UserRole Role,
  Guid? CompanyId
);
