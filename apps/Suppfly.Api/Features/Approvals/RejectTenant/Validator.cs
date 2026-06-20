using FluentValidation;

namespace Suppfly.Api.Features.Approvals.RejectTenant;

public class Validator : AbstractValidator<Command>
{
  public Validator()
  {
    RuleFor(x => x.RejectionReason)
      .MaximumLength(1000).WithMessage("Rejection reason cannot exceed 1000 characters.");
  }
}
