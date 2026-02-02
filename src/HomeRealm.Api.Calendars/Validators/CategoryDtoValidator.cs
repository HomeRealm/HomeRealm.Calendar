using HomeRealm.Api.Calendars.Dtos.Categories;
using FluentValidation;

namespace HomeRealm.Api.Calendars.Validators;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
  public CategoryDtoValidator()
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

