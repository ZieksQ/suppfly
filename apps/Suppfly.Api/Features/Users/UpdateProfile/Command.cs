using MediatR;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Features.Users.UpdateProfile;

public record Command(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
) : IRequest<Result<Response>>;

public record UpdateProfileRequest(
  string FirstName,
  string LastName,
  string Email
);
