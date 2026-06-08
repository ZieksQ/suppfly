using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.UpdateCompanyName;

public record Command(
  Guid Id,
  string Name,
  string Slug
) : IRequest<Result>;

public record RequestDto(
  string Name,
  string Slug
);
