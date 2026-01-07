using FamMan.Api.Calendars.Dtos.Attendee;
using FamMan.Api.Calendars.Validators;
using FluentValidation.TestHelper;

namespace FamMan.Tests.Calendars.UnitTests.Validators;

public class AttendeeRequestDtoValidatorTests
{
  private readonly AttendeeRequestDtoValidator _validator;

  public AttendeeRequestDtoValidatorTests()
  {
    _validator = new AttendeeRequestDtoValidator();
  }

  [Fact]
  public void Validate_WithValidDto_ShouldNotHaveErrors()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = "Confirmed",
      Role = "Guest"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveAnyValidationErrors();
  }

  [Fact]
  public void Validate_WithEmptyStatus_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = "",
      Role = "Guest"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Status);
  }

  [Fact]
  public void Validate_WithStatusExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = new string('a', 201),
      Role = "Guest"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Status);
  }

  [Fact]
  public void Validate_WithEmptyRole_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = "Confirmed",
      Role = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Role);
  }

  [Fact]
  public void Validate_WithRoleExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = "Confirmed",
      Role = new string('a', 201)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Role);
  }

  [Fact]
  public void Validate_WithEmptyEventId_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.Empty,
      UserId = Guid.NewGuid(),
      Status = "Confirmed",
      Role = "Guest"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.EventId);
  }

  [Fact]
  public void Validate_WithEmptyUserId_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new AttendeeRequestDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.Empty,
      Status = "Confirmed",
      Role = "Guest"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.UserId);
  }
}
