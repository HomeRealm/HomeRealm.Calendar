using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.RecurrenceRule;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.RecurrenceRule;

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
  public IQueryable<RecurrenceRuleEntity> GetAllRecurrenceRulesAsync(CancellationToken ct)
  {
    return _db.RecurrenceRules.AsNoTracking().AsQueryable();
  }

  public async Task DeleteRecurrenceRuleAsync(Guid id, CancellationToken ct)
  {
    await _db.RecurrenceRules.Where(rr => rr.Id == id).ExecuteDeleteAsync(ct);
  }
}
