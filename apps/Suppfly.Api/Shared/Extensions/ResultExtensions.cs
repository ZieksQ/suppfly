using Suppfly.Api.Shared.Response;

namespace Suppfly.Api.Shared.Extensions;

public static class ResultExtensions
{
  public static BaseResponse<T> ToResponse<T>(
      this Result<T> result, string message = "")
  {
    return new BaseResponse<T>(
          result.IsSuccess,
          message,
          result.Value,
          result.Error ?? null
      );
  }

  public static BaseResponse ToResponse(
      this Result result, string message = "")
  {
    return new BaseResponse(
          result.IsSuccess,
          message,
          result.Error ?? null
      );
  }
}
