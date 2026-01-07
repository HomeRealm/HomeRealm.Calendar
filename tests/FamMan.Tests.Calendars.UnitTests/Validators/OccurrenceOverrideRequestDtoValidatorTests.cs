using FamMan.Api.Calendars.Dtos.OccurrenceOverride;
using FamMan.Api.Calendars.Validators;
using FluentValidation.TestHelper;

namespace FamMan.Tests.Calendars.UnitTests.Validators;

public class OccurrenceOverrideRequestDtoValidatorTests
{
  private readonly OccurrenceOverrideRequestDtoValidator _validator;

  public OccurrenceOverrideRequestDtoValidatorTests()
  {
    _validator = new OccurrenceOverrideRequestDtoValidator();
  }

  [Fact]
  public void Validate_WithValidDto_ShouldNotHaveErrors()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new OccurrenceOverrideRequestDto
    {
      RecurrenceId = Guid.NewGuid(),
      Date = now
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveAnyValidationErrors();
  }

  [Fact]
  public void Validate_WithEmptyRecurrenceId_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new OccurrenceOverrideRequestDto
    {
      RecurrenceId = Guid.Empty,
      Date = now
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.RecurrenceId);
  }

  [Fact]
  public void Validate_WithMinValueDate_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new OccurrenceOverrideRequestDto
    {
      RecurrenceId = Guid.NewGuid(),
      Date = DateTime.MinValue
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Date);
  }
}
