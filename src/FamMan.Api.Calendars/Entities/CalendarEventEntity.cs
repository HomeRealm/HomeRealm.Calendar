namespace FamMan.Api.Calendars.Entities;

public class CalendarEventEntity
{
  public Guid Id { get; set; } // Primary Key
  public required Guid CalendarId { get; set; } // Foreign Key
  public required string Title { get; set; }
  public required string Description { get; set; }
  public required DateTime Start { get; set; }
  public required DateTime End { get; set; }
  public required string Location { get; set; }
  public bool AllDay { get; set; } = true;
  public required Guid RecurrenceId { get; set; } // Foreign Key
  public Guid? CategoryId { get; set; } // Foreign Key
  public string? LinkedResource { get; set; } = "";

  // Navigation properties
  public CalendarEntity Calendar { get; set; } = null!;
  public RecurrenceRuleEntity RecurrenceRule { get; set; } = null!;
  public CategoryEntity? Category { get; set; }
  public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
  public ICollection<ReminderEntity> Reminders { get; set; } = new List<ReminderEntity>();
}
