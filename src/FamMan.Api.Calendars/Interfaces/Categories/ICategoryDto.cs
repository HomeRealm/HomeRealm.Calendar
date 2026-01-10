using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces.Categories;

public interface ICategoryRequestDto
{
  [Description("Category Name")]
  public string Name { get; set; }
  [Description("Category Color")]
  public string Color { get; set; }
  [Description("Category Icon")]
  public string Icon { get; set; }
};

public interface ICategoryResponseDto : ICategoryRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}
