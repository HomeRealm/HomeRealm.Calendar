using FamMan.Api.Calendars.Interfaces.Calendars;

namespace FamMan.Api.Calendars.Dtos.Calendars;

/// <inheritdoc/>
public class CalendarResponseDto : ICalenderResponseDto
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required string Color { get; set; }
  public required string Owner { get; set; }
  public required string Visibility { get; set; }
}
