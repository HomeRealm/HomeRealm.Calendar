using FamMan.Api.Calendars.Interfaces.OccurrenceOverride;

namespace FamMan.Api.Calendars.Dtos.OccurrenceOverride;

public class OccurrenceOverrideResponseDto : IOccurrenceOverrideResponseDto
{
  public Guid Id { get; set; }
  public required Guid RecurrenceId { get; set; }
  public required DateTime Date { get; set; }
}
