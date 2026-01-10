namespace FamMan.Api.Calendars.Dtos.Reminders;

public record ReminderResponseDto : ReminderDto
{
  public Guid Id { get; set; }
}
