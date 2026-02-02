namespace HomeRealm.Api.Calendars.Entities;

public class ReminderEntity
{
  public Guid Id { get; set; } // Primary Key
  public required Guid EventId { get; set; } // Foreign Key
  public required string Method { get; set; }
  public required int TimeBefore { get; set; }

  // Navigation property
  public CalendarEventEntity CalendarEvent { get; set; } = null!;
}

