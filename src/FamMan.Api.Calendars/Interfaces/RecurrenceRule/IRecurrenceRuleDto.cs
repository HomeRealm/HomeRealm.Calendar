using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces.RecurrenceRule;

public interface IRecurrenceRuleRequestDto
{
  [Description("Event ID")]
  public Guid EventId { get; set; }
  [Description("Recurrence Rule")]
  public string Rule { get; set; }
  [Description("Occurrence Override IDs")]
  public List<Guid> OccurrenceOverrides { get; set; }
  [Description("Recurrence End Date")]
  public DateTime EndDate { get; set; }
};

public interface IRecurrenceRuleResponseDto : IRecurrenceRuleRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}
