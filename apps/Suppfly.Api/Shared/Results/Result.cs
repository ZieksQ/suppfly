namespace Suppfly.Api.Shared.Results;

public class Result<T>
{
  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;
  public T? Value { get; }
  public string Error { get; }

  private Result(T value)
  {
    IsSuccess = true;
    Value = value;
    Error = string.Empty;
  }

  private Result(string error)
  {
    IsSuccess = false;
    Value = default;
    Error = error;
  }

  public static Result<T> Ok(T value) => new(value);
  public static Result<T> Fail(string error) => new(error);

  // public ResultHttpMapper<T> OnSuccess(Func<T, IResult> handler) =>
  //   new ResultHttpMapper<T>(this).OnSuccess(handler);
  //
  // public ResultHttpMapper<T> OnFailure(Func<string, IResult> handler) =>
  //   new ResultHttpMapper<T>(this).OnFailure(handler);
}

public class Result
{
  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;
  public string Error { get; }

  private Result(bool success, string error)
  {
    IsSuccess = success;
    Error = error;
  }

  public static Result Ok() => new(true, string.Empty);
  public static Result Fail(string error) => new(false, error);
}

// public class ResultHttpMapper<T>
// {
//   private readonly Result<T> _result;
//   private Func<T, IResult>? _successHandler;
//   private Func<string, IResult>? _failureHandler;
//
//   public ResultHttpMapper(Result<T> result)
//   {
//     _result = result;
//   }
//
//   public ResultHttpMapper<T> OnSuccess(Func<T, IResult> handler)
//   {
//     _successHandler = handler;
//     return this;
//   }
//
//   public ResultHttpMapper<T> OnFailure(Func<string, IResult> handler)
//   {
//     _failureHandler = handler;
//     return this;
//   }
//
//   public IResult ToHttpResult()
//   {
//     if (_result.IsSuccess)
//     {
//       return _successHandler is not null
//         ? _successHandler(_result.Value!)
//         : Results.Ok(_result.ToResponse());
//     }
//
//     return _failureHandler is not null
//       ? _failureHandler(_result.Error)
//       : Results.BadRequest(_result.ToResponse());
//   }
// }
//
