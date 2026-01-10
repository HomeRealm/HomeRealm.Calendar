namespace FamMan.Api.Calendars.Dtos.OccurrenceOverrides;

/// <inheritdoc />
public record OccurrenceOverrideResponseDto : OccurrenceOverrideDto
{
  /// <summary>
  /// Unique identifier for the occurrence override record.
  /// </summary>
  public Guid Id { get; set; }
}
