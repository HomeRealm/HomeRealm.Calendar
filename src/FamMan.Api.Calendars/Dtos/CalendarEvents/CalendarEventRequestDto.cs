using FamMan.Api.Calendars.Interfaces.CalendarEvents;

namespace FamMan.Api.Calendars.Dtos.CalendarEvents;

public class CalendarEventRequestDto : ICalendarEventRequestDto
{
  public required Guid CalendarId { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public required DateTime Start { get; set; }
  public required DateTime End { get; set; }
  public required string Location { get; set; }
  public required bool AllDay { get; set; }
  public required Guid RecurrenceId { get; set; }
  public Guid? CategoryId { get; set; }
  public string? LinkedResource { get; set; }
}
