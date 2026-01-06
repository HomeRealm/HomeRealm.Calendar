namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarEventRequestDto
{
  public Guid CalendarId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime Start { get; set; }
  public DateTime End { get; set; }
  public string Location { get; set; }
  public bool AllDay { get; set; }
  public Guid RecurrenceId { get; set; }
  public Guid CategoryId { get; set; }
  public string LinkedResource { get; set; }
}

public interface ICalendarEventResponseDto : ICalendarEventRequestDto
{
  public Guid Id { get; set; }
}