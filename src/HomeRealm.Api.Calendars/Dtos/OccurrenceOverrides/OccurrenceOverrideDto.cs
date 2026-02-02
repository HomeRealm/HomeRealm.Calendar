namespace HomeRealm.Api.Calendars.Dtos.OccurrenceOverrides;

/// <summary>
/// Represents a single occurrence adjustment within a recurrence series.
/// </summary>
public record OccurrenceOverrideDto
{
  /// <summary>
  /// Identifier of the recurrence rule that owns this occurrence override.
  /// </summary>
  public required Guid RecurrenceId { get; init; }

  /// <summary>
  /// Date of the occurrence being overridden.
  /// </summary>
  public required DateTime Date { get; init; }
}
