using FluentValidation;

namespace Suppfly.Api.Features.Companies.CreateCompany;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Company Name is required.")
      .MaximumLength(255).WithMessage("Company Name cannot exceed 255 characters.");

    RuleFor(x => x.Slug)
      .NotEmpty().WithMessage("Company Slug is required.")
      .MaximumLength(255).WithMessage("Company Slug cannot excceed 255 characters.");

    RuleFor(x => x.Type)
      .IsInEnum().WithMessage("Company Type must be valid.");

    RuleFor(x => x.TaxId)
      .MaximumLength(100).WithMessage("Tax Id cannot exceed 100 characters.");

    RuleFor(x => x.Tier)
      .IsInEnum().WithMessage("Company Tier must be valid.");
  }
}
