using MediatR;

namespace Suppfly.Api.Features.Auth.Logout;

public sealed record Command(
  string RefreshToken
) : IRequest;
