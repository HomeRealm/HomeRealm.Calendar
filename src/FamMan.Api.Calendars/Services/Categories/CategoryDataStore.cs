using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Categories;

public class CategoryDataStore : ICategoryDataStore
{
  private readonly CalendarDbContext _db;
  public CategoryDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<CategoryEntity> CreateCategoryAsync(CategoryEntity entity, CancellationToken ct)
  {
    await _db.Categories.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<CategoryEntity> UpdateCategoryAsync(CategoryEntity existingEntity, CategoryEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<CategoryEntity?> GetCategoryAsync(Guid id, CancellationToken ct)
  {
    return await _db.Categories.FindAsync(id, ct);
  }
  public IQueryable<CategoryEntity> GetAllCategoriesAsync(CancellationToken ct)
  {
    return _db.Categories.AsNoTracking().AsQueryable();
  }

  public async Task DeleteCategoryAsync(Guid id, CancellationToken ct)
  {
    await _db.Categories.Where(c => c.Id == id).ExecuteDeleteAsync(ct);
  }
}
