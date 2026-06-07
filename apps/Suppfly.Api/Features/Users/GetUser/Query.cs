using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Users.GetUser;

public record Query(
  Guid? Id,
  bool IncludeUser
) : IRequest<Result<Response>>;
