using System.ComponentModel;

namespace FamMan.Api.Chores.Dtos;

/// <summary>
/// Represents a chore with its details
/// </summary>
public record ChoreDto
{
  [Description("Unique identifier for the chore")]
  public Guid Id { get; init; }

  [Description("Name of the chore")]
  public required string Name { get; init; }

  [Description("Detailed description of the chore")]
  public required string Description { get; init; }

  [Description("Date and time when the chore was created")]
  public DateTimeOffset CreatedAt { get; init; }

  [Description("Date and time when the chore is due")]
  public DateTimeOffset DueDate { get; init; }
}
