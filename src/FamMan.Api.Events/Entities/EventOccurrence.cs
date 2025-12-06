namespace FamMan.Api.Events.Entities;

public class EventOccurrence
{
	public Guid Id { get; set; }
	public Guid ActionableEventId { get; set; }
	public DateTime ScheduledTime { get; set; }
	public required string Status { get; set; } // "pending", "completed", "cancelled", "failed"
	public DateTime? ExecutedAt { get; set; }
	public int AttemptCount { get; set; } = 0;
	public string? LastError { get; set; }
	public DateTime CreatedAt { get; set; }
	
	// Navigation property
	public ActionableEvent ActionableEvent { get; set; } = default!;
}
