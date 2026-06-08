namespace Suppfly.Api.Shared.Response;

public record PagedResponse<T>(
  bool Success,
  string Message,
  T? Data,
  string? Errors,
  MetaData? MetaData
) : BaseResponse<T>(Success, Message, Data, Errors);

public record MetaData(
  int PageNumber,
  int PageSize,
  int TotalPage,
  int TotalRecords
);

public class PagedList<T>
{
  public IEnumerable<T> Items { get; set; } = [];
  public MetaData? Meta { get; set; }

  public PagedList(
      IEnumerable<T> items,
      int pageNumber,
      int pageSize,
      int totalRecords)
  {
    Items = items;
    Meta = new MetaData(
        pageNumber,
        pageSize,
        (int)Math.Ceiling((double)totalRecords / pageSize),
        totalRecords
    );
  }
}

