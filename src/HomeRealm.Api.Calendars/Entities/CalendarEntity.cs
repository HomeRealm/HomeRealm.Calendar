namespace HomeRealm.Api.Calendars.Entities;

public class CalendarEntity
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required string Color { get; set; }
  public required string Owner { get; set; }
  public required string Visibility { get; set; }

  // Navigation property
  public ICollection<CalendarEventEntity> CalendarEvents { get; set; } = new List<CalendarEventEntity>();
}

