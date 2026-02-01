namespace FamMan.Api.Calendars.Dtos.Reminders;

/// <inheritdoc />
public record ReminderResponseDto : ReminderDto
{
  /// <summary>
  /// Unique identifier for the reminder record.
  /// </summary>
  public Guid Id { get; init; }
}
