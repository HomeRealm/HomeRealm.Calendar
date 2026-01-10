using FamMan.Api.Calendars.Dtos.RecurrenceRules;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class RecurrenceRuleRequestDtoValidator : AbstractValidator<RecurrenceRuleRequestDto>
{
  public RecurrenceRuleRequestDtoValidator()
  {
    RuleFor(x => x.EventId)
      .NotEmpty().WithMessage("EventId cannot be empty.");

    RuleFor(x => x.Rule)
      .NotEmpty().WithMessage("Rule cannot be empty.")
      .MaximumLength(200).WithMessage("Rule must be shorter than 200 characters.");

    RuleFor(x => x.OccurrenceOverrides)
      .NotNull().WithMessage("OccurrenceOverrides cannot be null.");

    RuleFor(x => x.EndDate)
      .NotEmpty().WithMessage("EndDate cannot be empty.");
  }
}
