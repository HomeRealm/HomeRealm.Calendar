namespace FamMan.Api.Calendars.Dtos.CalendarEvents;

public record CalendarEventResponseDto : CalendarEventDto
{
  public Guid Id { get; set; }
}
