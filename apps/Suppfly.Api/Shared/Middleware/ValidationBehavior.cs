using FluentValidation;
using MediatR;
using Suppfly.Api.Shared.Results;

namespace Suppfly.Api.Shared.Middleware;

public class ValidationBehavior<TRequest, TResponse>
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;
  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }
  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    if (!_validators.Any())
      return await next();
    var context = new ValidationContext<TRequest>(request);
    var failures = _validators
      .Select(v => v.Validate(context))
      .SelectMany(r => r.Errors)
      .Where(f => f != null)
      .ToList();
    if (failures.Count > 0)
    {
      var errors = string.Join("; ", failures.Select(f => f.ErrorMessage));
      var responseType = typeof(TResponse);
      if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
      {
        var failMethod = responseType.GetMethod("Fail", [typeof(string)]);
        return (TResponse)failMethod!.Invoke(null, [errors])!;
      }
      if (responseType == typeof(Result))
      {
        return (TResponse)(object)Result.Fail(errors);
      }
      throw new ValidationException(failures);
    }
    return await next();
  }
}
