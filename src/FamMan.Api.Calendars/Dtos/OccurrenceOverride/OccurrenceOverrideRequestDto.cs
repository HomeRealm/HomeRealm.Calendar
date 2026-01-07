using FamMan.Api.Calendars.Interfaces.OccurrenceOverride;

namespace FamMan.Api.Calendars.Dtos.OccurrenceOverride;

public class OccurrenceOverrideRequestDto : IOccurrenceOverrideRequestDto
{
  public required Guid RecurrenceId { get; set; }
  public required DateTime Date { get; set; }
}
