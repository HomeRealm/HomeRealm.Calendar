namespace FamMan.Api.Calendars.Dtos.Attendees;

public record AttendeeDto
{
  public required Guid EventId { get; set; }
  public required Guid UserId { get; set; }
  public required string Status { get; set; }
  public required string Role { get; set; }
}
