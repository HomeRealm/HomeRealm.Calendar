namespace FamMan.Api.Calendars.Entities;

public class AttendeeEntity
{
  public Guid Id { get; set; } // Primary Key
  public required Guid EventId { get; set; } // Foreign Key
  public required Guid UserId { get; set; }
  public string Status { get; set; } = "Confirmed";
  public required string Role { get; set; }

  // Navigation property
  public CalendarEventEntity CalendarEvent { get; set; } = null!;
}
