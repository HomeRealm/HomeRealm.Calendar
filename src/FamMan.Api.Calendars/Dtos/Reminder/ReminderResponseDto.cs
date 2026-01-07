using FamMan.Api.Calendars.Interfaces.Reminder;

namespace FamMan.Api.Calendars.Dtos.Reminder;

public class ReminderResponseDto : IReminderResponseDto
{
  public Guid Id { get; set; }
  public required Guid EventId { get; set; }
  public required string Method { get; set; }
  public required int TimeBefore { get; set; }
}
