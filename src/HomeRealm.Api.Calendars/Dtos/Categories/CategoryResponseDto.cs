namespace HomeRealm.Api.Calendars.Dtos.Categories;

/// <inheritdoc />
public record CategoryResponseDto : CategoryDto
{
  /// <summary>
  /// Unique identifier for the category record.
  /// </summary>
  public Guid Id { get; init; }
}
