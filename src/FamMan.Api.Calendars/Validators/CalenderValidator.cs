using FamMan.Api.Calendars.Entities;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class CalendarValidator : AbstractValidator<CalendarEntity>
{
  public CalendarValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(x => x.Description)
      .NotEmpty()
      .MaximumLength(1000);

    RuleFor(x => x.Color)
      .NotEmpty()
      .MaximumLength(50);

    RuleFor(x => x.Owner)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.Visibility)
      .NotEmpty()
      .MaximumLength(10);
  }
}
