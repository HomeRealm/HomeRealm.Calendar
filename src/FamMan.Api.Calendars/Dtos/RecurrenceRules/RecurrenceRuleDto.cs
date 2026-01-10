namespace FamMan.Api.Calendars.Dtos.RecurrenceRules;

/// <summary>
/// Represents recurrence rule details applied to a calendar event series.
/// </summary>
public record RecurrenceRuleDto
{
  /// <summary>
  /// Identifier of the event this recurrence rule belongs to.
  /// </summary>
  public required Guid EventId { get; set; }

  /// <summary>
  /// Recurrence rule expression describing the repetition pattern.
  /// </summary>
  public required string Rule { get; set; }

  /// <summary>
  /// Collection of occurrence overrides that modify specific instances of the recurrence.
  /// </summary>
  public required List<Guid> OccurrenceOverrides { get; set; }

  /// <summary>
  /// Date when the recurrence rule ends.
  /// </summary>
  public required DateTime EndDate { get; set; }
}
