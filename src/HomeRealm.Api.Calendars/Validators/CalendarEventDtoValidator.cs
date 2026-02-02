using HomeRealm.Api.Calendars.Dtos.CalendarEvents;
using FluentValidation;

namespace HomeRealm.Api.Calendars.Validators;

public class CalendarEventDtoValidator : AbstractValidator<CalendarEventDto>
{
  public CalendarEventDtoValidator()
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
      .NotEmpty().WithMessage("End date cannot be empty.")
      .GreaterThan(x => x.Start).WithMessage("End must be after Start.");

    RuleFor(x => x.Location)
      .NotEmpty().WithMessage("Location cannot be empty.")
      .MaximumLength(200).WithMessage("Location must be shorter than 200 characters.");


    RuleFor(x => x.LinkedResource)
      .MaximumLength(200).WithMessage("LinkedResource must be shorter than 200 characters.")
      .When(x => !string.IsNullOrWhiteSpace(x.LinkedResource));
  }
}

