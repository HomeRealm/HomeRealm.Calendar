using FamMan.Api.Calendars.Interfaces.Attendees;

namespace FamMan.Api.Calendars.Dtos.Attendees;

public class AttendeeResponseDto : IAttendeeResponseDto
{
  public Guid Id { get; set; }
  public required Guid EventId { get; set; }
  public required Guid UserId { get; set; }
  public required string Status { get; set; }
  public required string Role { get; set; }
}
