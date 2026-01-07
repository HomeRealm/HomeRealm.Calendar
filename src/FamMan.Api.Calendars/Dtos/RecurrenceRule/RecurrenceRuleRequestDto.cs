using FamMan.Api.Calendars.Interfaces.RecurrenceRule;

namespace FamMan.Api.Calendars.Dtos.RecurrenceRule;

public class RecurrenceRuleRequestDto : IRecurrenceRuleRequestDto
{
  public required Guid EventId { get; set; }
  public required string Rule { get; set; }
  public required List<Guid> OccurrenceOverrides { get; set; }
  public required DateTime EndDate { get; set; }
}
