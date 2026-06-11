using FluentValidation;

namespace Suppfly.Api.Features.Auth.RegisterB2BAccount;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty().WithMessage("First name is required.")
      .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

    RuleFor(x => x.LastName)
      .NotEmpty().WithMessage("Last name is required.")
      .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("Email format is not valid.");

    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MinimumLength(8).WithMessage("Password must at least have 8 characters.")
      .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
      .Matches("[0-9]").WithMessage("Password must contain at least one number.");

    RuleFor(x => x.CompanyName)
      .NotEmpty().WithMessage("Company name is required.")
      .MaximumLength(255).WithMessage("Company name cannot exceed 255 characters.");

    RuleFor(x => x.CompanySlug)
      .NotEmpty().WithMessage("Company slug is required.")
      .Must(s => !s.Any(char.IsUpper)).WithMessage("Company slug cannot have uppercase letter.")
      .Matches(@"^[a-z0-9-]+$").WithMessage("Slug cannot contain spaces or special characters execpt for \'-\'")
      .MaximumLength(255).WithMessage("Company slug caannot exceed 255 characters.");

    RuleFor(x => x.CompanyType)
      .IsInEnum().WithMessage("Company Type is not valid.");

    RuleFor(x => x.TaxId)
      .MaximumLength(100).WithMessage("Company tax id cannot exceed 100 characters.");

    RuleFor(x => x.CompanyTier)
      .IsInEnum().WithMessage("Company tier is not valid.");
  }
}
