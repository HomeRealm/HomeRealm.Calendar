namespace FamMan.Api.Chores.Entities;

public class Chore
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset DueDate { get; set; }
}
