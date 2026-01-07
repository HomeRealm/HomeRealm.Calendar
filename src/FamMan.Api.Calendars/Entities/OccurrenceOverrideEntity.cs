namespace FamMan.Api.Calendars.Entities;

public class OccurrenceOverrideEntity
{
  public Guid Id { get; set; } // Primary Key
  public required Guid RecurrenceId { get; set; } // Foreign Key
  public required DateTime Date { get; set; }

  // Navigation property
  public RecurrenceRuleEntity RecurrenceRule { get; set; } = null!;
}
