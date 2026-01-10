using FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;

namespace FamMan.Api.Calendars.Dtos.OccurrenceOverrides;

public class OccurrenceOverrideRequestDto : IOccurrenceOverrideRequestDto
{
  public required Guid RecurrenceId { get; set; }
  public required DateTime Date { get; set; }
}
