namespace FamMan.Api.Calendars.Dtos.Reminders;

public record ReminderDto
{
  public required Guid EventId { get; set; }
  public required string Method { get; set; }
  public required int TimeBefore { get; set; }
}
