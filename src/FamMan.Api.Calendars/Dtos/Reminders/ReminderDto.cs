namespace FamMan.Api.Calendars.Dtos.Reminders;

/// <summary>
/// Represents reminder settings used to notify participants about an event.
/// </summary>
public record ReminderDto
{
  /// <summary>
  /// Identifier of the event the reminder is tied to.
  /// </summary>
  public required Guid EventId { get; set; }

  /// <summary>
  /// Delivery method for the reminder (for example, email or notification).
  /// </summary>
  public required string Method { get; set; }

  /// <summary>
  /// Amount of time before the event start when the reminder should be sent, expressed in minutes.
  /// </summary>
  public required int TimeBefore { get; set; }
}
