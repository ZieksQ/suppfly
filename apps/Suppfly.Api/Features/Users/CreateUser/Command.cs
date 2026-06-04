using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Features.Users.CreateUser;

public record Command(
    Guid CompanyId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserRole Role
    ) : IRequest<Result<Response>>;
