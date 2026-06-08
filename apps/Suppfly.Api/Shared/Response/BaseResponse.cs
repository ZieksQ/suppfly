namespace Suppfly.Api.Shared.Response;

public record BaseResponse(
  bool Success,
  string Message,
  string? Errors
);

public record BaseResponse<T>(
  bool Success,
  string Message,
  T? Data,
  string? Errors
) : BaseResponse(Success, Message, Errors);
