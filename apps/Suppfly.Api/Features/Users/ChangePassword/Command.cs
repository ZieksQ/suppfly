using MediatR;
using Suppfly.Api.Shared;
using Suppfly.Api.Shared.Response;

namespace Suppfly.Api.Features.Users.ChangePassword;

public record Command(
    Guid Id,
    string CurrentPassword,
    string NewPassword
) : IRequest<Result>;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);
