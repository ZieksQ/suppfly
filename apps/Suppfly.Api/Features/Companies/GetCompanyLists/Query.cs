using MediatR;
using Suppfly.Api.Domain.Enums;
using Suppfly.Api.Shared.Response;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Features.Companies.GetCompanyLists;

public record Query(
  int PageNumber,
  int PageSize,
  bool IncludeUsers
) : IRequest<Result<PagedList<Response>>>;

public record Response(
  Guid Id,
  string Name,
  string Slug,
  CompanyType Type,
  string? TaxId,
  CompanyStatus Status,
  CompanyTier Tier,
  Guid? ApprovedByUserId,
  DateTime? ApprovedAt,
  DateTime CreatedAt,
  DateTime UpdatedAt
);
