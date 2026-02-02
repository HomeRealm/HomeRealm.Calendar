using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.RecurrenceRules;
using Microsoft.EntityFrameworkCore;

namespace HomeRealm.Api.Calendars.Services.RecurrenceRules;

public class RecurrenceRuleDataStore : IRecurrenceRuleDataStore
{
  private readonly CalendarDbContext _db;
  public RecurrenceRuleDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<RecurrenceRuleEntity> CreateRecurrenceRuleAsync(RecurrenceRuleEntity entity, CancellationToken ct)
  {
    await _db.RecurrenceRules.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<RecurrenceRuleEntity> UpdateRecurrenceRuleAsync(RecurrenceRuleEntity existingEntity, RecurrenceRuleEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<RecurrenceRuleEntity?> GetRecurrenceRuleAsync(Guid id, CancellationToken ct)
  {
    return await _db.RecurrenceRules.FindAsync(id, ct);
  }
  public IQueryable<RecurrenceRuleEntity> GetRecurrenceRulesForCalendarEventAsync(Guid id)
  {
    return _db.RecurrenceRules
      .Where(rr => rr.EventId == id)
      .AsNoTracking();
  }
  public IQueryable<RecurrenceRuleEntity> GetAllRecurrenceRules()
  {
    return _db.RecurrenceRules.AsNoTracking().AsQueryable();
  }

  public async Task DeleteRecurrenceRuleAsync(Guid id, CancellationToken ct)
  {
    await _db.RecurrenceRules.Where(rr => rr.Id == id).ExecuteDeleteAsync(ct);
  }
}
