namespace FamMan.Api.Calendars.Dtos.Calendars;

public record CalendarDto
{
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required string Color { get; set; }
  public required string Owner { get; set; }
  public required string Visibility { get; set; }
}
