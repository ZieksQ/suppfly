using FluentValidation;

namespace Suppfly.Api.Features.Users.ChangePassword;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(u => u.CurrentPassword)
      .NotEmpty().WithMessage("Current Password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
      .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
      .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
      .Matches("[0-9]").WithMessage("Password must contain at least one number.");

    RuleFor(u => u.NewPassword)
      .NotEmpty().WithMessage("Current Password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
      .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
      .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
      .Matches("[0-9]").WithMessage("Password must contain at least one number.");
  }
}
