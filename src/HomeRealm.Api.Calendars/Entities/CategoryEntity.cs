namespace HomeRealm.Api.Calendars.Entities;

public class CategoryEntity
{
  public Guid Id { get; set; } // Primary Key
  public required string Name { get; set; }
  public required string Color { get; set; }
  public string? Icon { get; set; }

  // Navigation property
  public ICollection<CalendarEventEntity> CalendarEvents { get; set; } = new List<CalendarEventEntity>();
}
