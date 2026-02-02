using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.OccurrenceOverrides;
using Microsoft.EntityFrameworkCore;

namespace HomeRealm.Api.Calendars.Services.OccurrenceOverrides;

public class OccurrenceOverrideDataStore : IOccurrenceOverrideDataStore
{
  private readonly CalendarDbContext _db;
  public OccurrenceOverrideDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<OccurrenceOverrideEntity> CreateOccurrenceOverrideAsync(OccurrenceOverrideEntity entity, CancellationToken ct)
  {
    await _db.OccurrenceOverrides.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<OccurrenceOverrideEntity> UpdateOccurrenceOverrideAsync(OccurrenceOverrideEntity existingEntity, OccurrenceOverrideEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<OccurrenceOverrideEntity?> GetOccurrenceOverrideAsync(Guid id, CancellationToken ct)
  {
    return await _db.OccurrenceOverrides.FindAsync(id, ct);
  }
  public IQueryable<OccurrenceOverrideEntity> GetOccurrenceOverridesForRecurrenceRule(Guid id)
  {
    return _db.OccurrenceOverrides.Where(oo => oo.RecurrenceId == id).AsNoTracking().AsQueryable();
  }
  public IQueryable<OccurrenceOverrideEntity> GetAllOccurrenceOverrides()
  {
    return _db.OccurrenceOverrides.AsNoTracking().AsQueryable();
  }
  public async Task DeleteOccurrenceOverrideAsync(Guid id, CancellationToken ct)
  {
    await _db.OccurrenceOverrides.Where(oo => oo.Id == id).ExecuteDeleteAsync(ct);
  }
}
