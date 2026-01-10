namespace FamMan.Api.Calendars.Dtos.Categories;

public record CategoryResponseDto : CategoryDto
{
  public Guid Id { get; set; }
}
