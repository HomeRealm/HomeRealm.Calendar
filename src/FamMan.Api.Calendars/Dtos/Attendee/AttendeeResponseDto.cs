using FamMan.Api.Calendars.Interfaces.Attendee;

namespace FamMan.Api.Calendars.Dtos.Attendee;

public class AttendeeResponseDto : IAttendeeResponseDto
{
  public Guid Id { get; set; }
  public required Guid EventId { get; set; }
  public required Guid UserId { get; set; }
  public required string Status { get; set; }
  public required string Role { get; set; }
}
