using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Calendars;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Calendars;

public class CalendarDataStore : ICalendarDataStore
{
  private readonly CalendarDbContext _db;
  public CalendarDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<CalendarEntity> CreateCalendarAsync(CalendarEntity entity, CancellationToken ct)
  {
    await _db.Calendars.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<CalendarEntity> UpdateCalendarAsync(CalendarEntity existingEntity, CalendarEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<CalendarEntity?> GetCalendarAsync(Guid id, CancellationToken ct)
  {
    return await _db.Calendars.FindAsync(id, ct);
  }
  public IQueryable<CalendarEntity> GetAllCalendarsAsync(CancellationToken ct)
  {
    return _db.Calendars.AsNoTracking().AsQueryable();
  }

  public async Task DeleteCalendarAsync(Guid id, CancellationToken ct)
  {
    await _db.Calendars.Where(c => c.Id == id).ExecuteDeleteAsync(ct);
  }
}
