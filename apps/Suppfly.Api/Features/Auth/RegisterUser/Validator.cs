using FluentValidation;

namespace Suppfly.Api.Features.Auth.RegisterUser;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(u => u.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Email format is not valid.");

    RuleFor(u => u.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
      .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
      .Matches("[0-9]").WithMessage("Password must contain at least one number.");

    RuleFor(u => u.FirstName)
      .NotEmpty().WithMessage("First name is required.")
      .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

    RuleFor(u => u.LastName)
      .NotEmpty().WithMessage("Last name is required.")
      .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

    RuleFor(u => u.Role)
      .IsInEnum().WithMessage("Role is not valid.");
  }
}
