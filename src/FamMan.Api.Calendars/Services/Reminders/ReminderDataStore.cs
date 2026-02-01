using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Reminders;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Reminders;

public class ReminderDataStore : IReminderDataStore
{
  private readonly CalendarDbContext _db;
  public ReminderDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<ReminderEntity> CreateReminderAsync(ReminderEntity entity, CancellationToken ct)
  {
    await _db.Reminders.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<ReminderEntity> UpdateReminderAsync(ReminderEntity existingEntity, ReminderEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<ReminderEntity?> GetReminderAsync(Guid id, CancellationToken ct)
  {
    return await _db.Reminders.FindAsync(id, ct);
  }
  public IQueryable<ReminderEntity> GetRemindersForCalendarEvent(Guid id)
  {
    return _db.Reminders.Where(r => r.EventId == id).AsNoTracking().AsQueryable();
  }
  public IQueryable<ReminderEntity> GetAllReminders()
  {
    return _db.Reminders.AsNoTracking().AsQueryable();
  }
  public async Task DeleteReminderAsync(Guid id, CancellationToken ct)
  {
    await _db.Reminders.Where(r => r.Id == id).ExecuteDeleteAsync(ct);
  }
}
