namespace FamMan.Api.Calendars.Dtos.Calendars;

public record CalendarResponseDto : CalendarDto
{
  public Guid Id { get; set; }
}
