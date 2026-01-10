using FamMan.Api.Calendars.Dtos.Reminders;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class ReminderDtoValidator : AbstractValidator<ReminderDto>
{
  public ReminderDtoValidator()
  {
    RuleFor(x => x.EventId)
      .NotEmpty().WithMessage("EventId cannot be empty.");

    RuleFor(x => x.Method)
      .NotEmpty().WithMessage("Method cannot be empty.")
      .MaximumLength(200).WithMessage("Method must be shorter than 200 characters.");

    RuleFor(x => x.TimeBefore)
      .GreaterThanOrEqualTo(0).WithMessage("TimeBefore must be greater than or equal to 0.")
      .NotEmpty().WithMessage("TimeBefore cannot be empty.");
  }
}

