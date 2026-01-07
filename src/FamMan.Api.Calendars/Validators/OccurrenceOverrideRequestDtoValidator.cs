using FamMan.Api.Calendars.Dtos.OccurrenceOverride;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class OccurrenceOverrideRequestDtoValidator : AbstractValidator<OccurrenceOverrideRequestDto>
{
  public OccurrenceOverrideRequestDtoValidator()
  {
    RuleFor(x => x.RecurrenceId)
      .NotEmpty().WithMessage("RecurrenceId cannot be empty.");

    RuleFor(x => x.Date)
      .NotEmpty().WithMessage("Date cannot be empty.");
  }
}
