namespace FamMan.Api.Events.Entities;

public class ActionableEvent
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public required string EventType { get; set; }
	public required string Description { get; set; }
	
	// Recurrence configuration stored as JSON
	public required string RecurrenceRules { get; set; }
	
	// Event status and tracking
	public bool IsActive { get; set; } = true;
	
	// Audit fields
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
