using FamMan.Api.Calendars.Interfaces.RecurrenceRules;

namespace FamMan.Api.Calendars.Dtos.RecurrenceRules;

public class RecurrenceRuleResponseDto : IRecurrenceRuleResponseDto
{
  public Guid Id { get; set; }
  public required Guid EventId { get; set; }
  public required string Rule { get; set; }
  public required List<Guid> OccurrenceOverrides { get; set; }
  public required DateTime EndDate { get; set; }
}
