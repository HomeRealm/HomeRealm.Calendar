using FamMan.Api.Calendars.Interfaces.Category;

namespace FamMan.Api.Calendars.Dtos.Category;

public class CategoryRequestDto : ICategoryRequestDto
{
  public required string Name { get; set; }
  public required string Color { get; set; }
  public required string Icon { get; set; }
}
