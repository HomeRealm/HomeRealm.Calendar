namespace FamMan.Api.Calendars.Dtos.RecurrenceRules;

public record RecurrenceRuleDto
{
  public required Guid EventId { get; set; }
  public required string Rule { get; set; }
  public required List<Guid> OccurrenceOverrides { get; set; }
  public required DateTime EndDate { get; set; }
}
