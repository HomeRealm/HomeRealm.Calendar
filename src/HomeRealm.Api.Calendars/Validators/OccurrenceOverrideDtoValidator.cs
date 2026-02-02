using HomeRealm.Api.Calendars.Dtos.OccurrenceOverrides;
using FluentValidation;

namespace HomeRealm.Api.Calendars.Validators;

public class OccurrenceOverrideDtoValidator : AbstractValidator<OccurrenceOverrideDto>
{
  public OccurrenceOverrideDtoValidator()
  {
    RuleFor(x => x.RecurrenceId)
      .NotEmpty().WithMessage("RecurrenceId cannot be empty.");

    RuleFor(x => x.Date)
      .NotEmpty().WithMessage("Date cannot be empty.");
  }
}

