using FluentValidation;

namespace Suppfly.Api.Features.Companies.UpdateTaxId;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(c => c.TaxId)
      .NotEmpty().WithMessage("Tax Id should not be empty.")
      .MaximumLength(100).WithMessage("Tax Id cannot exceed 100 characters.");
  }
}
