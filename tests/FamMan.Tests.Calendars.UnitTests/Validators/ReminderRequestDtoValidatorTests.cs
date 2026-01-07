using FamMan.Api.Calendars.Dtos.Reminder;
using FamMan.Api.Calendars.Validators;
using FluentValidation.TestHelper;

namespace FamMan.Tests.Calendars.UnitTests.Validators;

public class ReminderRequestDtoValidatorTests
{
  private readonly ReminderRequestDtoValidator _validator;

  public ReminderRequestDtoValidatorTests()
  {
    _validator = new ReminderRequestDtoValidator();
  }

  [Fact]
  public void Validate_WithValidDto_ShouldNotHaveErrors()
  {
    // Arrange
    var dto = new ReminderRequestDto
    {
      EventId = Guid.NewGuid(),
      Method = "Email",
      TimeBefore = 15
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveAnyValidationErrors();
  }

  [Fact]
  public void Validate_WithEmptyMethod_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new ReminderRequestDto
    {
      EventId = Guid.NewGuid(),
      Method = "",
      TimeBefore = 15
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Method);
  }

  [Fact]
  public void Validate_WithMethodExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new ReminderRequestDto
    {
      EventId = Guid.NewGuid(),
      Method = new string('a', 201),
      TimeBefore = 15
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Method);
  }

  [Fact]
  public void Validate_WithEmptyEventId_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new ReminderRequestDto
    {
      EventId = Guid.Empty,
      Method = "Email",
      TimeBefore = 15
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.EventId);
  }

  [Fact]
  public void Validate_WithNegativeTimeBefore_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new ReminderRequestDto
    {
      EventId = Guid.NewGuid(),
      Method = "Email",
      TimeBefore = -1
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.TimeBefore);
  }
}
