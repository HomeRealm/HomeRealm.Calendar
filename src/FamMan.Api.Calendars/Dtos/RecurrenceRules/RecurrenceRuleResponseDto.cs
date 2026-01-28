namespace FamMan.Api.Calendars.Dtos.RecurrenceRules;

/// <inheritdoc />
public record RecurrenceRuleResponseDto : RecurrenceRuleDto
{
  /// <summary>
  /// Unique identifier for the recurrence rule record.
  /// </summary>
  public Guid Id { get; init; }
}
