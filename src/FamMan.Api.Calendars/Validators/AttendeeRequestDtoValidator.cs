using FamMan.Api.Calendars.Dtos.Attendees;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class AttendeeRequestDtoValidator : AbstractValidator<AttendeeRequestDto>
{
  public AttendeeRequestDtoValidator()
  {
    RuleFor(x => x.EventId)
      .NotEmpty().WithMessage("EventId cannot be empty.");

    RuleFor(x => x.UserId)
      .NotEmpty().WithMessage("UserId cannot be empty.");

    RuleFor(x => x.Status)
      .NotEmpty().WithMessage("Status cannot be empty.")
      .MaximumLength(200).WithMessage("Status must be shorter than 200 characters.");

    RuleFor(x => x.Role)
      .NotEmpty().WithMessage("Role cannot be empty.")
      .MaximumLength(200).WithMessage("Role must be shorter than 200 characters.");
  }
}
