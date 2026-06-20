using FluentValidation;

namespace Suppfly.Api.Features.Auth.Login;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.Email)
      .NotNull().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Email format is not valid.");

    RuleFor(x => x.Password)
      .NotNull().WithMessage("Password is required.")
      .Matches("[A-Z]").WithMessage("Password must have at least one Uppercase letter.")
      .Matches("[0-9]").WithMessage("Password must have at least one Number.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
  }
}
