using MediatR;
using Suppfly.Api.Shared.Results;
using Suppfly.Api.Shared.Response;

namespace Suppfly.Api.Features.Users.GetUsersList;

public record Query(
  int PageNumber,
  int PageSize,
  bool IncludeCompany
) : IRequest<Result<PagedList<Response>>>;
