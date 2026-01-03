using FamMan.Api.Calendars.Dtos;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class CalendarRequestDtoValidator : AbstractValidator<CalendarRequestDto>
{
  public CalendarRequestDtoValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Name cannot be empty.")
      .MaximumLength(200).WithMessage("Name must be shorter than 200 characters.");

    RuleFor(x => x.Description)
      .NotEmpty().WithMessage("Description cannot be empty.")
      .MaximumLength(1000).WithMessage("Description must be shorter than 1000 characters.");

    RuleFor(x => x.Color)
      .NotEmpty().WithMessage("Color cannot be empty.")
      .MaximumLength(50).WithMessage("Color must be shorter than 50 characters.");

    RuleFor(x => x.Owner)
      .NotEmpty().WithMessage("Owner cannot be empty.")
      .MaximumLength(100).WithMessage("Owner must be shorter than 100 characters.");

    RuleFor(x => x.Visibility)
      .NotEmpty().WithMessage("Visibility cannot be empty.")
      .MaximumLength(10).WithMessage("Visibility must be shorter than 10 characters.");
  }
}
