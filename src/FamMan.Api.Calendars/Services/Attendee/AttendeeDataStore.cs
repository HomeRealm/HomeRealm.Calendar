using FamMan.Api.Calendars.Interfaces.Attendee;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Attendee;

public class AttendeeDataStore : IAttendeeDataStore
{
  private readonly CalendarDbContext _db;
  public AttendeeDataStore(CalendarDbContext db)
  {
    _db = db;
  }
  public async Task<Entities.AttendeeEntity> CreateAttendeeAsync(Entities.AttendeeEntity entity, CancellationToken ct)
  {
    await _db.Attendees.AddAsync(entity, ct);
    await _db.SaveChangesAsync(ct);
    return entity;
  }
  public async Task<Entities.AttendeeEntity> UpdateAttendeeAsync(Entities.AttendeeEntity existingEntity, Entities.AttendeeEntity updatedEntity, CancellationToken ct)
  {
    _db.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
    await _db.SaveChangesAsync(ct);
    return existingEntity;
  }
  public async Task<Entities.AttendeeEntity?> GetAttendeeAsync(Guid id, CancellationToken ct)
  {
    return await _db.Attendees.FindAsync(id, ct);
  }
  public IQueryable<Entities.AttendeeEntity> GetAllAttendeesAsync(CancellationToken ct)
  {
    return _db.Attendees.AsNoTracking().AsQueryable();
  }

  public async Task DeleteAttendeeAsync(Guid id, CancellationToken ct)
  {
    await _db.Attendees.Where(a => a.Id == id).ExecuteDeleteAsync(ct);
  }
}
