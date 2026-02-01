namespace FamMan.Api.Calendars.Dtos.CalendarEvents;

/// <summary>
/// Represents the details of a calendar event used when creating or updating an event.
/// </summary>
public record CalendarEventDto
{
  /// <summary>
  /// Identifier of the calendar that owns the event.
  /// </summary>
  public required Guid CalendarId { get; init; }

  /// <summary>
  /// Title or subject of the event.
  /// </summary>
  public required string Title { get; init; }

  /// <summary>
  /// Description providing additional context for the event.
  /// </summary>
  public required string Description { get; init; }

  /// <summary>
  /// Start date and time of the event.
  /// </summary>
  public required DateTime Start { get; init; }

  /// <summary>
  /// End date and time of the event.
  /// </summary>
  public required DateTime End { get; init; }

  /// <summary>
  /// Location where the event will occur.
  /// </summary>
  public required string Location { get; init; }

  /// <summary>
  /// Indicates whether the event spans the entire day.
  /// </summary>
  public required bool AllDay { get; init; }

  /// <summary>
  /// Optional identifier of the recurrence rules this event belongs to.
  /// Null for non-recurring (one-time) events, non-null for recurring events.
  /// </summary>
  public Guid? RecurrenceId { get; init; }

  /// <summary>
  /// Optional category used to classify the event.
  /// </summary>
  public Guid? CategoryId { get; init; }

  /// <summary>
  /// Optional external resource linked to the event (for example, meeting URL).
  /// </summary>
  public string? LinkedResource { get; init; }
}
