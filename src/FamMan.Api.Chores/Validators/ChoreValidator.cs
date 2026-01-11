using FluentValidation;
using FamMan.Api.Chores.Dtos;

namespace FamMan.Api.Chores.Validators;

public class ChoreValidator : AbstractValidator<ChoreDto>
{
  public ChoreValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Chore name is required.");

    RuleFor(x => x.Description)
        .NotEmpty().WithMessage("Chore description is required.");

    RuleFor(x => x.CreatedAt)
        .Must(BeUtc).WithMessage("CreatedAt must be in UTC (offset 0). Use DateTimeOffset.UtcNow.");

    RuleFor(x => x.DueDate)
        .Must(BeUtc).WithMessage("DueDate must be in UTC (offset 0). Use DateTimeOffset.UtcNow.");

    RuleFor(x => x)
        .Must(x => x.DueDate > x.CreatedAt)
        .WithMessage("DueDate must be after CreatedAt.");
  }

  private bool BeUtc(DateTimeOffset date)
  {
    return date.Offset == TimeSpan.Zero;
  }
}
