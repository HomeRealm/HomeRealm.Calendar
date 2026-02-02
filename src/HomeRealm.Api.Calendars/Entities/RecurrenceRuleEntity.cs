namespace HomeRealm.Api.Calendars.Entities;

public class RecurrenceRuleEntity
{
  public Guid Id { get; set; } // Primary Key
  public Guid EventId { get; set; } // Foreign Key
  public required string Rule { get; set; }
  public List<Guid> OccurrenceOverrides { get; set; } = [];
  public required DateTime EndDate { get; set; }

  // Navigation properties
  public CalendarEventEntity CalendarEvent { get; set; } = null!;
  public ICollection<OccurrenceOverrideEntity> OccurrenceOverrideEntities { get; set; } = new List<OccurrenceOverrideEntity>();
}

