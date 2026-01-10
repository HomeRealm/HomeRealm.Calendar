namespace FamMan.Api.Calendars.Dtos.Calendars;

/// <summary>
/// Represents details required to create or update a calendar.
/// </summary>
public record CalendarDto
{
  /// <summary>
  /// Display name of the calendar.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Description that explains the purpose or scope of the calendar.
  /// </summary>
  public required string Description { get; set; }

  /// <summary>
  /// Color identifier used to visually distinguish the calendar.
  /// </summary>
  public required string Color { get; set; }

  /// <summary>
  /// Owner of the calendar.
  /// </summary>
  public required string Owner { get; set; }

  /// <summary>
  /// Visibility setting that defines who can view the calendar.
  /// </summary>
  public required string Visibility { get; set; }
}
