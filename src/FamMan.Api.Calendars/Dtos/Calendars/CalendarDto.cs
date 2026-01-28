namespace FamMan.Api.Calendars.Dtos.Calendars;

/// <summary>
/// Represents details required to create or update a calendar.
/// </summary>
public record CalendarDto
{
  /// <summary>
  /// Display name of the calendar.
  /// </summary>
  public required string Name { get; init; }

  /// <summary>
  /// Description that explains the purpose or scope of the calendar.
  /// </summary>
  public required string Description { get; init; }

  /// <summary>
  /// Color identifier used to visually distinguish the calendar.
  /// </summary>
  public required string Color { get; init; }

  /// <summary>
  /// Owner of the calendar.
  /// </summary>
  public required string Owner { get; init; }

  /// <summary>
  /// Visibility setting that defines who can view the calendar.
  /// </summary>
  public required string Visibility { get; init; }
}
