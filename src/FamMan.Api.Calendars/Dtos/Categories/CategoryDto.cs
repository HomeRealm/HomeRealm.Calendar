namespace FamMan.Api.Calendars.Dtos.Categories;

/// <summary>
/// Represents a category definition used to organize calendar events.
/// </summary>
public record CategoryDto
{
  /// <summary>
  /// Display name of the category.
  /// </summary>
  public required string Name { get; init; }

  /// <summary>
  /// Color identifier used to visually distinguish the category.
  /// </summary>
  public required string Color { get; init; }

  /// <summary>
  /// Optional icon representing the category.
  /// </summary>
  public string? Icon { get; init; }
}
