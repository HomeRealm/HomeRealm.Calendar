using FamMan.Api.Calendars.Dtos.Categories;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Categories;

public class CategoryService : ICategoryService
{
  private readonly ICategoryDataStore _dataStore;
  public CategoryService(ICategoryDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateCategoryAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, CategoryResponseDto? updatedCategory)> UpdateCategoryAsync(CategoryRequestDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetCategoryAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateCategoryAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, CategoryResponseDto? category)> GetCategoryAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetCategoryAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync(CancellationToken ct)
  {
    var categories = _dataStore.GetAllCategoriesAsync(ct);

    var mappedCategories = await categories.Select(category => MapToResponseDto(category)).ToListAsync(ct);
    return mappedCategories;
  }
  public async Task DeleteCategoryAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteCategoryAsync(id, ct);
  }
  private CategoryEntity MapToEntity(CategoryRequestDto dto, Guid? id = null)
  {
    return new CategoryEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      Name = dto.Name,
      Color = dto.Color,
      Icon = dto.Icon
    };
  }
  private CategoryResponseDto MapToResponseDto(CategoryEntity entity)
  {
    return new CategoryResponseDto
    {
      Id = entity.Id,
      Name = entity.Name,
      Color = entity.Color,
      Icon = entity.Icon
    };
  }
}
