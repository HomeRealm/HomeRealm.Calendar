using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.Attendees;

public interface IAttendeeDataStore
{
  public Task<AttendeeEntity> CreateAttendeeAsync(AttendeeEntity entity, CancellationToken ct);
  public Task<AttendeeEntity> UpdateAttendeeAsync(AttendeeEntity existingEntity, AttendeeEntity updatedEntity, CancellationToken ct);
  public Task<AttendeeEntity?> GetAttendeeAsync(Guid id, CancellationToken ct);
  public IQueryable<AttendeeEntity> GetAttendeesForCalendarEvent(Guid id);
  public IQueryable<AttendeeEntity> GetAllAttendeesAsync(CancellationToken ct);
  public Task DeleteAttendeeAsync(Guid id, CancellationToken ct);
}
