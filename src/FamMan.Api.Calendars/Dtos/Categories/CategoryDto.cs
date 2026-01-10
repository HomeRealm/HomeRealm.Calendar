namespace FamMan.Api.Calendars.Dtos.Categories;

public record CategoryDto
{
  public required string Name { get; set; }
  public required string Color { get; set; }
  public string? Icon { get; set; }
}
