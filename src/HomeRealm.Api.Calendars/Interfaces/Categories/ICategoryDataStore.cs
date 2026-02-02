using HomeRealm.Api.Calendars.Entities;

namespace HomeRealm.Api.Calendars.Interfaces.Categories;

public interface ICategoryDataStore
{
  public Task<CategoryEntity> CreateCategoryAsync(CategoryEntity entity, CancellationToken ct);
  public Task<CategoryEntity> UpdateCategoryAsync(CategoryEntity existingEntity, CategoryEntity updatedEntity, CancellationToken ct);
  public Task<CategoryEntity?> GetCategoryAsync(Guid id, CancellationToken ct);
  public IQueryable<CategoryEntity> GetAllCategories();
  public Task DeleteCategoryAsync(Guid id, CancellationToken ct);
}

