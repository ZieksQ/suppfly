namespace Suppfly.Api.Shared;

public class PaginationResponse<T>
{
  public IEnumerable<T> Data { get; set; }
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
  public int TotalPage { get; set; }
  public int TotalRecords { get; set; }

  public PaginationResponse(
      IEnumerable<T> data,
      int pageNumber,
      int pageSize,
      int totalPage,
      int totalRecords
  )
  {
    Data = data;
    PageNumber = pageNumber;
    PageSize = pageSize;
    TotalPage = totalPage;
    TotalRecords = totalRecords;
  }
}
