namespace FamMan.Api.Calendars.Dtos.OccurrenceOverrides;

public record OccurrenceOverrideDto
{
  public required Guid RecurrenceId { get; set; }
  public required DateTime Date { get; set; }
}
