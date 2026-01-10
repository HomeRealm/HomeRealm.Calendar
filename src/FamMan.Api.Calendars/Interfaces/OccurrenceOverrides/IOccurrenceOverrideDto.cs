using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;

public interface IOccurrenceOverrideRequestDto
{
  [Description("Recurrence Rule ID")]
  public Guid RecurrenceId { get; set; }
  [Description("Override Date")]
  public DateTime Date { get; set; }
};

public interface IOccurrenceOverrideResponseDto : IOccurrenceOverrideRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}
