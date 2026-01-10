using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.Attendee;

public interface IAttendeeDataStore
{
  public Task<Entities.AttendeeEntity> CreateAttendeeAsync(Entities.AttendeeEntity entity, CancellationToken ct);
  public Task<Entities.AttendeeEntity> UpdateAttendeeAsync(Entities.AttendeeEntity existingEntity, Entities.AttendeeEntity updatedEntity, CancellationToken ct);
  public Task<Entities.AttendeeEntity?> GetAttendeeAsync(Guid id, CancellationToken ct);
  public IQueryable<Entities.AttendeeEntity> GetAllAttendeesAsync(CancellationToken ct);
  public Task DeleteAttendeeAsync(Guid id, CancellationToken ct);
}
