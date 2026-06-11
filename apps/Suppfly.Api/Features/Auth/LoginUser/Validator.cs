using FluentValidation;

namespace Suppfly.Api.Features.Auth.LoginUser;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(u => u.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Email format is not valid.");

    RuleFor(u => u.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
  }
}
