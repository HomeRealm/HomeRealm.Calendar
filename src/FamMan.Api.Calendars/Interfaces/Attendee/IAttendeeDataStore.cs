using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.Attendee;

public interface IAttendeeDataStore
{
  public Task<Entities.Attendee> CreateAttendeeAsync(Entities.Attendee entity, CancellationToken ct);
  public Task<Entities.Attendee> UpdateAttendeeAsync(Entities.Attendee existingEntity, Entities.Attendee updatedEntity, CancellationToken ct);
  public Task<Entities.Attendee?> GetAttendeeAsync(Guid id, CancellationToken ct);
  public IQueryable<Entities.Attendee> GetAllAttendeesAsync(CancellationToken ct);
  public Task DeleteAttendeeAsync(Guid id, CancellationToken ct);
}
