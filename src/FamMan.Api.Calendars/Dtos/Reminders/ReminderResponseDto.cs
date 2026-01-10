using FamMan.Api.Calendars.Interfaces.Reminders;

namespace FamMan.Api.Calendars.Dtos.Reminders;

public class ReminderResponseDto : IReminderResponseDto
{
  public Guid Id { get; set; }
  public required Guid EventId { get; set; }
  public required string Method { get; set; }
  public required int TimeBefore { get; set; }
}
