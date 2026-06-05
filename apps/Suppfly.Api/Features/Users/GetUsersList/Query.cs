using MediatR;
using Suppfly.Api.Shared;

namespace Suppfly.Api.Features.Users.GetUsersList;

public record Query(
  int PageNumber,
  int PageSize,
  bool IncludeCompany
) : IRequest<Result<PaginationResponse<Response>>>;
