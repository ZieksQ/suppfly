using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.RegisterTenant;

public sealed record Command(
  string Email,
  string Password,
  string FirstName,
  string LastName,
  string CompanyName
) : IRequest<Result<Guid>>;
