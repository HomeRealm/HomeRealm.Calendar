namespace FamMan.Api.Events.Dtos;

public class EventOccurrenceDto
{
	public Guid Id { get; set; }
	public Guid ActionableEventId { get; set; }
	public required string Name { get; set; }
	public required string Type { get; set; }
	public required string Description { get; set; }
	public DateTime ScheduledTime { get; set; }
	public required string Status { get; set; }
	public DateTime? ExecutedAt { get; set; }
	public int AttemptCount { get; set; }
	public string? LastError { get; set; }
	public DateTime CreatedAt { get; set; }
}
