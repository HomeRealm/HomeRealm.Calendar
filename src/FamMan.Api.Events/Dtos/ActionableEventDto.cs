using System.Text.Json;

namespace FamMan.Api.Events.Dtos;

public class ActionableEventDto
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public required string EventType { get; set; }
	public required string Description { get; set; }
	public required JsonElement RecurrenceRules { get; set; }
	public bool IsActive { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
