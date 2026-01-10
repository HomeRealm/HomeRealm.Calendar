using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces.CalendarEvents;

public interface ICalendarEventRequestDto
{
  [Description("Calendar ID")]
  public Guid CalendarId { get; set; }
  [Description("Event Title")]
  public string Title { get; set; }
  [Description("Event Description")]
  public string Description { get; set; }
  [Description("Start Date and Time")]
  public DateTime Start { get; set; }
  [Description("End Date and Time")]
  public DateTime End { get; set; }
  [Description("Event Location")]
  public string Location { get; set; }
  [Description("All Day Event")]
  public bool AllDay { get; set; }
  [Description("Recurrence Rule ID")]
  public Guid RecurrenceId { get; set; }
  [Description("Category ID")]
  public Guid? CategoryId { get; set; }
  [Description("Linked Resource")]
  public string? LinkedResource { get; set; }
};

public interface ICalendarEventResponseDto : ICalendarEventRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}