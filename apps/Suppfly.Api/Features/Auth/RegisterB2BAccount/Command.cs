using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Auth.RegisterB2BAccount;

public record Command(
  string FirstName,
  string LastName,
  string Email,
  string Password,
  string CompanyName,
  string CompanySlug,
  CompanyType CompanyType,
  string? TaxId,
  CompanyTier CompanyTier
) : IRequest<Result<Guid>>;
