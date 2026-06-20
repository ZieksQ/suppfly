using MediatR;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Approvals.GetPendingTenant;

public sealed record Query(
  int PageNumber,
  int PageSize
) : IRequest<Result<PagedList<Response>>>;

public sealed record Response(
  CompanyDto Company
);

public sealed record CompanyDto(
  string Name,
  string Status,
  UserDto CreatedByUser,
  DateTime CreatedAt
);

public sealed record UserDto(
  Guid Id,
  string Email,
  string FirstName,
  string LastName
);
