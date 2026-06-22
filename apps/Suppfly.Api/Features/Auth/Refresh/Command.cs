using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.Refresh;

public sealed record Command(
  string RefreshToken
) : IRequest<Result<string>>;
