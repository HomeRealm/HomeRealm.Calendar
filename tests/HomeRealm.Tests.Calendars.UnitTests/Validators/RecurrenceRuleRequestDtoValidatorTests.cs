using HomeRealm.Api.Calendars.Dtos.RecurrenceRules;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation.TestHelper;

namespace HomeRealm.Tests.Calendars.UnitTests.Validators;

public class RecurrenceRuleDtoValidatorTests
{
  private readonly RecurrenceRuleDtoValidator _validator;

  public RecurrenceRuleDtoValidatorTests()
  {
    _validator = new RecurrenceRuleDtoValidator();
  }

  [Fact]
  public void Validate_WithValidDto_ShouldNotHaveErrors()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new RecurrenceRuleDto
    {
      EventId = Guid.NewGuid(),
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveAnyValidationErrors();
  }

  [Fact]
  public void Validate_WithEmptyRule_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new RecurrenceRuleDto
    {
      EventId = Guid.NewGuid(),
      Rule = "",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Rule);
  }

  [Fact]
  public void Validate_WithRuleExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new RecurrenceRuleDto
    {
      EventId = Guid.NewGuid(),
      Rule = new string('a', 201),
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Rule);
  }

  [Fact]
  public void Validate_WithEmptyEventId_ShouldHaveValidationError()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var dto = new RecurrenceRuleDto
    {
      EventId = Guid.Empty,
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.EventId);
  }

  [Fact]
  public void Validate_WithMinValueEndDate_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new RecurrenceRuleDto
    {
      EventId = Guid.NewGuid(),
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = DateTime.MinValue
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.EndDate);
  }
}
