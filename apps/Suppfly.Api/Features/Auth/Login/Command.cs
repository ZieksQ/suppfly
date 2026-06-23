using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.Login;

public sealed record Command(
  string Email,
  string Password
) : IRequest<Result<Response>>;

public sealed record Response(
  string AccessToken,
  string RefreshToken
);

// public sealed record UserDto(
//   string FirstName,
//   string LastName,
//   string Email,
//   string? GlobalRole,
//   IEnumerable<Companies> Companies
// );
//
// public sealed record Companies(
//   Guid CompanyId,
//   string CompanyName,
//   string Role,
//   DateTime JoinedAt
// );
