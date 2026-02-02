using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.Attendees;
using Microsoft.EntityFrameworkCore;

namespace HomeRealm.Api.Calendars.Services.Attendees;

public class AttendeeDataStore : IAttendeeDataStore
{
  private readonly CalendarDbContext _db;
  public AttendeeDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<AttendeeEntity> CreateAttendeeAsync(AttendeeEntity entity, CancellationToken ct)
  {
    await _db.Attendees.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<AttendeeEntity> UpdateAttendeeAsync(AttendeeEntity existingEntity, AttendeeEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<AttendeeEntity?> GetAttendeeAsync(Guid id, CancellationToken ct)
  {
    return await _db.Attendees.FindAsync(id, ct);
  }
  public IQueryable<AttendeeEntity> GetAttendeesForCalendarEvent(Guid id)
  {
    return _db.Attendees.Where(a => a.EventId == id).AsNoTracking().AsQueryable();
  }
  public IQueryable<AttendeeEntity> GetAllAttendees()
  {
    return _db.Attendees.AsNoTracking().AsQueryable();
  }

  public async Task DeleteAttendeeAsync(Guid id, CancellationToken ct)
  {
    await _db.Attendees.Where(a => a.Id == id).ExecuteDeleteAsync(ct);
  }
}
