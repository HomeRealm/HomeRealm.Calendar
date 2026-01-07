using FamMan.Api.Calendars.Dtos.Reminder;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class ReminderRequestDtoValidator : AbstractValidator<ReminderRequestDto>
{
  public ReminderRequestDtoValidator()
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
