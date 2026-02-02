using HomeRealm.Api.Calendars.Dtos.Calendars;
using FluentValidation;

namespace HomeRealm.Api.Calendars.Validators;

public class CalendarDtoValidator : AbstractValidator<CalendarDto>
{
  public CalendarDtoValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Name cannot be empty.");

    RuleFor(x => x.Description)
      .NotEmpty().WithMessage("Description cannot be empty.");

    RuleFor(x => x.Color)
      .NotEmpty().WithMessage("Color cannot be empty.");

    RuleFor(x => x.Owner)
      .NotEmpty().WithMessage("Owner cannot be empty.");

    RuleFor(x => x.Visibility)
      .NotEmpty().WithMessage("Visibility cannot be empty.");
  }
}
