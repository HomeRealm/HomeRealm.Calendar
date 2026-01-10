using FamMan.Api.Calendars.Interfaces.Categories;

namespace FamMan.Api.Calendars.Dtos.Categories;

public class CategoryRequestDto : ICategoryRequestDto
{
  public required string Name { get; set; }
  public required string Color { get; set; }
  public required string Icon { get; set; }
}
