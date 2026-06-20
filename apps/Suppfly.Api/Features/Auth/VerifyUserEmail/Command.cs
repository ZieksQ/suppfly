using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.VerifyUserEmail;

public sealed record Command(
  Guid Id
) : IRequest<Result<Response>>;

public sealed record Response(
  Guid Id,
  string Email,
  bool IsEmailVerified,
  bool IsActive,
  DateTime UpdatedAt
);
