namespace FamMan.Api.Calendars.Dtos.CalendarEvents;

/// <inheritdoc />
public record CalendarEventResponseDto : CalendarEventDto
{
  /// <summary>
  /// Unique identifier for the calendar event record.
  /// </summary>
  public Guid Id { get; init; }
}
