using FluentValidation;

namespace Suppfly.Api.Features.Approvals.RejectRequest;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.Notes)
      .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.");
  }
}
