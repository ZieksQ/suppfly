using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.RegisterUser;

public record Command(
  string FirstName,
  string LastName,
  string Email,
  string Password,
  UserRole Role,
  Guid CompanyId
) : IRequest<Result<Guid>>;
