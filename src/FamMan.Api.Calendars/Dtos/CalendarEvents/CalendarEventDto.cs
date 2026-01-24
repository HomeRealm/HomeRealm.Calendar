namespace FamMan.Api.Calendars.Dtos.CalendarEvents;

/// <summary>
/// Represents the details of a calendar event used when creating or updating an event.
/// </summary>
public record CalendarEventDto
{
  /// <summary>
  /// Identifier of the calendar that owns the event.
  /// </summary>
  public required Guid CalendarId { get; set; }

  /// <summary>
  /// Title or subject of the event.
  /// </summary>
  public required string Title { get; set; }

  /// <summary>
  /// Description providing additional context for the event.
  /// </summary>
  public required string Description { get; set; }

  /// <summary>
  /// Start date and time of the event.
  /// </summary>
  public required DateTime Start { get; set; }

  /// <summary>
  /// End date and time of the event.
  /// </summary>
  public required DateTime End { get; set; }

  /// <summary>
  /// Location where the event will occur.
  /// </summary>
  public required string Location { get; set; }

  /// <summary>
  /// Indicates whether the event spans the entire day.
  /// </summary>
  public required bool AllDay { get; set; }

  /// <summary>
  /// Optional identifier of the recurrence rules this event belongs to.
  /// Null for non-recurring (one-time) events, non-null for recurring events.
  /// </summary>
  public Guid? RecurrenceId { get; set; }

  /// <summary>
  /// Optional category used to classify the event.
  /// </summary>
  public Guid? CategoryId { get; set; }

  /// <summary>
  /// Optional external resource linked to the event (for example, meeting URL).
  /// </summary>
  public string? LinkedResource { get; set; }
}
