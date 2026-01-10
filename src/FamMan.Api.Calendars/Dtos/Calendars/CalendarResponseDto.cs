namespace FamMan.Api.Calendars.Dtos.Calendars;

/// <inheritdoc />
public record CalendarResponseDto : CalendarDto
{
  /// <summary>
  /// Unique identifier for the calendar record.
  /// </summary>
  public Guid Id { get; set; }
}
