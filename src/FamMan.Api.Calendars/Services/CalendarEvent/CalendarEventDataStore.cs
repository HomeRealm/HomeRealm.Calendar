using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.CalendarEvent;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.CalendarEvent;

public class CalendarEventDataStore : ICalendarEventDataStore
{
  private readonly CalendarDbContext _db;
  public CalendarEventDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<CalendarEventEntity> CreateCalendarEventAsync(CalendarEventEntity entity, CancellationToken ct)
  {
    await _db.CalendarEvents.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<CalendarEventEntity> UpdateCalendarEventAsync(CalendarEventEntity existingEntity, CalendarEventEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<CalendarEventEntity?> GetCalendarEventAsync(Guid id, CancellationToken ct)
  {
    return await _db.CalendarEvents.FindAsync(id, ct);
  }
  public IQueryable<CalendarEventEntity> GetAllCalendarEventsAsync(CancellationToken ct)
  {
    return _db.CalendarEvents.AsNoTracking().AsQueryable();
  }

  public async Task DeleteCalendarEventAsync(Guid id, CancellationToken ct)
  {
    await _db.CalendarEvents.Where(ce => ce.Id == id).ExecuteDeleteAsync(ct);
  }
}
