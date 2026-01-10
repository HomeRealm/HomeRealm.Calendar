using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces.Attendees;

public interface IAttendeeRequestDto
{
  [Description("Event ID")]
  public Guid EventId { get; set; }
  [Description("User ID")]
  public Guid UserId { get; set; }
  [Description("Attendee Status")]
  public string Status { get; set; }
  [Description("Attendee Role")]
  public string Role { get; set; }
};

public interface IAttendeeResponseDto : IAttendeeRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}
