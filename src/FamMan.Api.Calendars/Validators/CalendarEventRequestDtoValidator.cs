using FamMan.Api.Calendars.Dtos.CalendarEvent;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class CalendarEventRequestDtoValidator : AbstractValidator<CalendarEventRequestDto>
{
  public CalendarEventRequestDtoValidator()
  {
    RuleFor(x => x.CalendarId)
      .NotEmpty().WithMessage("CalendarId cannot be empty.");

    RuleFor(x => x.Title)
      .NotEmpty().WithMessage("Title cannot be empty.")
      .MaximumLength(200).WithMessage("Title must be shorter than 200 characters.");

    RuleFor(x => x.Description)
      .NotEmpty().WithMessage("Description cannot be empty.")
      .MaximumLength(200).WithMessage("Description must be shorter than 200 characters.");

    RuleFor(x => x.Start)
      .NotEmpty().WithMessage("Start cannot be empty.");

    RuleFor(x => x.End)
      .NotEmpty().WithMessage("End cannot be empty.");

    RuleFor(x => x.Location)
      .NotEmpty().WithMessage("Location cannot be empty.")
      .MaximumLength(200).WithMessage("Location must be shorter than 200 characters.");

    RuleFor(x => x.RecurrenceId)
      .NotEmpty().WithMessage("RecurrenceId cannot be empty.");

    RuleFor(x => x.LinkedResource)
      .MaximumLength(200).WithMessage("LinkedResource must be shorter than 200 characters.");
  }
}
