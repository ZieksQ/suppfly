using FluentValidation;

namespace Suppfly.Api.Features.Auth.RegisterTenant;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is requried.")
      .EmailAddress().WithMessage("Email format is not valid.");

    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required.")
      .Matches("[A-Z]").WithMessage("Password must have at least one Uppercase letter.")
      .Matches("[0-9]").WithMessage("Password must have at least one Number.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.");

    RuleFor(x => x.FirstName)
      .NotEmpty().WithMessage("First name is required.")
      .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

    RuleFor(x => x.LastName)
      .NotEmpty().WithMessage("Last name is required.")
      .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

    RuleFor(x => x.CompanyName)
      .NotEmpty().WithMessage("Company name is required.")
      .MaximumLength(255).WithMessage("Company name cannot exceed 255 characters.");
  }
}
