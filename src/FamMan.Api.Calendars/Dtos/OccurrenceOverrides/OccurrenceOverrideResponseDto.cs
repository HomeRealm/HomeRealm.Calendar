namespace FamMan.Api.Calendars.Dtos.OccurrenceOverrides;

public record OccurrenceOverrideResponseDto : OccurrenceOverrideDto
{
  public Guid Id { get; set; }
}
