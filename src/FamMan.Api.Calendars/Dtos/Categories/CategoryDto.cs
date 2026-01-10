namespace FamMan.Api.Calendars.Dtos.Categories;

/// <summary>
/// Represents a category definition used to organize calendar events.
/// </summary>
public record CategoryDto
{
  /// <summary>
  /// Display name of the category.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Color identifier used to visually distinguish the category.
  /// </summary>
  public required string Color { get; set; }

  /// <summary>
  /// Optional icon representing the category.
  /// </summary>
  public string? Icon { get; set; }
}
