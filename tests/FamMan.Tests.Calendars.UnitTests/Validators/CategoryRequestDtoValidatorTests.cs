using FamMan.Api.Calendars.Dtos.Category;
using FamMan.Api.Calendars.Validators;
using FluentValidation.TestHelper;

namespace FamMan.Tests.Calendars.UnitTests.Validators;

public class CategoryRequestDtoValidatorTests
{
  private readonly CategoryRequestDtoValidator _validator;

  public CategoryRequestDtoValidatorTests()
  {
    _validator = new CategoryRequestDtoValidator();
  }

  [Fact]
  public void Validate_WithValidDto_ShouldNotHaveErrors()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = "Work",
      Color = "Blue",
      Icon = "briefcase"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldNotHaveAnyValidationErrors();
  }

  [Fact]
  public void Validate_WithEmptyName_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = "",
      Color = "Blue",
      Icon = "briefcase"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Name);
  }

  [Fact]
  public void Validate_WithNameExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = new string('a', 201),
      Color = "Blue",
      Icon = "briefcase"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Name);
  }

  [Fact]
  public void Validate_WithEmptyColor_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = "Work",
      Color = "",
      Icon = "briefcase"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Color);
  }

  [Fact]
  public void Validate_WithColorExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = "Work",
      Color = new string('a', 201),
      Icon = "briefcase"
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Color);
  }

  [Fact]
  public void Validate_WithEmptyIcon_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = "Work",
      Color = "Blue",
      Icon = ""
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Icon);
  }

  [Fact]
  public void Validate_WithIconExceedingMaxLength_ShouldHaveValidationError()
  {
    // Arrange
    var dto = new CategoryRequestDto
    {
      Name = "Work",
      Color = "Blue",
      Icon = new string('a', 201)
    };

    // Act & Assert
    _validator.TestValidate(dto).ShouldHaveValidationErrorFor(x => x.Icon);
  }
}
