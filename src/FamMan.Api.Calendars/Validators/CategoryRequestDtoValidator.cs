using FamMan.Api.Calendars.Dtos.Category;
using FluentValidation;

namespace FamMan.Api.Calendars.Validators;

public class CategoryRequestDtoValidator : AbstractValidator<CategoryRequestDto>
{
  public CategoryRequestDtoValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty().WithMessage("Name cannot be empty.")
      .MaximumLength(200).WithMessage("Name must be shorter than 200 characters.");

    RuleFor(x => x.Color)
      .NotEmpty().WithMessage("Color cannot be empty.")
      .MaximumLength(200).WithMessage("Color must be shorter than 200 characters.");

    RuleFor(x => x.Icon)
      .NotEmpty().WithMessage("Icon cannot be empty.")
      .MaximumLength(200).WithMessage("Icon must be shorter than 200 characters.");
  }
}
