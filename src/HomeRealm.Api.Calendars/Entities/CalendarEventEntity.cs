namespace HomeRealm.Api.Calendars.Entities;

/// <summary>
/// Represents a calendar event aggregate root.
/// Events can be recurring (linked to a RecurrenceRule) or non-recurring (one-time events).
/// </summary>
public class CalendarEventEntity
{
  /// <summary>Primary key identifier for the event.</summary>
  public Guid Id { get; set; }

  /// <summary>Required foreign key to the parent Calendar aggregate.</summary>
  public required Guid CalendarId { get; set; }

  /// <summary>Event title/name.</summary>
  public required string Title { get; set; }

  /// <summary>Event description or details.</summary>
  public required string Description { get; set; }

  /// <summary>Event start date and time (UTC).</summary>
  public required DateTime Start { get; set; }

  /// <summary>Event end date and time (UTC). Must be after Start.</summary>
  public required DateTime End { get; set; }

  /// <summary>Event location or meeting details.</summary>
  public required string Location { get; set; }

  /// <summary>Indicates whether this is an all-day event.</summary>
  public bool AllDay { get; set; } = true;

  /// <summary>
  /// Optional foreign key to recurrence rule.
  /// Null indicates a non-recurring (one-time) event.
  /// Non-null indicates a recurring event following the associated recurrence rule.
  /// </summary>
  public Guid? RecurrenceId { get; set; }

  /// <summary>Optional foreign key to event category.</summary>
  public Guid? CategoryId { get; set; }

  /// <summary>Optional link to external resource (e.g., meeting URL, document).</summary>
  public string? LinkedResource { get; set; } = "";

  // Navigation properties
  public CalendarEntity Calendar { get; set; } = null!;
  public ICollection<RecurrenceRuleEntity> RecurrenceRules { get; set; } = new List<RecurrenceRuleEntity>();
  public CategoryEntity? Category { get; set; }
  public ICollection<AttendeeEntity> Attendees { get; set; } = new List<AttendeeEntity>();
  public ICollection<ReminderEntity> Reminders { get; set; } = new List<ReminderEntity>();
}

