using FamMan.Api.Calendars.Interfaces;

namespace FamMan.Api.Calendars.Dtos.CalendarEvent;

public class CalendarEventRequestDto : ICalendarEventRequestDto
{
  public required Guid CalendarId { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public required DateTime Start { get; set; }
  public required DateTime End { get; set; }
  public required string Location { get; set; }
  public bool AllDay { get; set; } = true;
  public required Guid RecurrenceId { get; set; }
  public required Guid CategoryId { get; set; }
  public string LinkedResource { get; set; } = "";
}
