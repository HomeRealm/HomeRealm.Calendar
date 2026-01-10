using FamMan.Api.Calendars.Interfaces.Categories;

namespace FamMan.Api.Calendars.Dtos.Categories;

public class CategoryResponseDto : ICategoryResponseDto
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Color { get; set; }
  public required string Icon { get; set; }
}
