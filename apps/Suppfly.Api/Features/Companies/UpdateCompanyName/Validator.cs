using FluentValidation;

namespace Suppfly.Api.Features.Companies.UpdateCompanyName;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Company Name is required.")
      .MaximumLength(255).WithMessage("Company Name cannot exceed 255 characters.");

    RuleFor(x => x.Slug)
      .NotEmpty().WithMessage("Company Slug is required.")
      .Must(s => !s.Any(char.IsUpper)).WithMessage("Slug must be lowercase only.")
      .Matches(@"^[a-z0-9-]+$").WithMessage("Slug cannot contain spaces or special characters execpt for \'-\'")
      .MaximumLength(255).WithMessage("Company Slug cannot excceed 255 characters.");
  }
}
