using HomeRealm.Api.Calendars.Dtos.CalendarEvents;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation.TestHelper;

namespace HomeRealm.Tests.Calendars.UnitTests.Validators;

public class CalendarEventDtoValidatorTests
{
  private readonly CalendarEventDtoValidator _validator;

  public CalendarEventDtoValidatorTests()
  {
    _validator = new CalendarEventDtoValidator();
  }

  [Fact]
  public void Validate_WithValidDto_ShouldNotHaveErrors()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveAnyValidationErrors();
  }

  [Fact]
  public void Validate_WithEmptyTitle_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Title);
  }

  [Fact]
  public void Validate_WithTitleExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = new string('a', 201),
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Title);
  }

  [Fact]
  public void Validate_WithDescriptionExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = new string('a', 501),
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Description);
  }

  [Fact]
  public void Validate_WithLocationExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = new string('a', 201),
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Location);
  }

  [Fact]
  public void Validate_WithLinkedResourceExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = new string('a', 201)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.LinkedResource);
  }

  [Fact]
  public void Validate_WithEmptyCalendarId_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.Empty,
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.CalendarId);
  }

  [Fact]
  public void Validate_WithNullRecurrenceId_ShouldNotHaveValidationError()
  {
    // Arrange - Non-recurring event without recurrence rule
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "One-Time Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = null, // Non-recurring event
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveValidationErrorFor(x => x.RecurrenceId);
  }

  [Fact]
  public void Validate_WithEndBeforeStart_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new CalendarEventDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now.AddHours(1),
      End = now, // End before Start
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.End);
  }
}
