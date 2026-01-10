using FamMan.Api.Calendars.Dtos.Categories;

namespace FamMan.Api.Calendars.Interfaces.Categories;

public interface ICategoryService
{
  public Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto dto, CancellationToken ct);
  public Task<(string status, CategoryResponseDto? updatedCategory)> UpdateCategoryAsync(CategoryRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string status, CategoryResponseDto? category)> GetCategoryAsync(Guid id, CancellationToken ct);
  public Task<List<CategoryResponseDto>> GetAllCategoriesAsync(CancellationToken ct);
  public Task DeleteCategoryAsync(Guid id, CancellationToken ct);
}
