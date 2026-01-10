namespace FamMan.Api.Calendars.Dtos.Attendees;

public record AttendeeResponseDto : AttendeeDto
{
  public Guid Id { get; set; }
}
