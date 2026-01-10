using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.Calendars;

public interface ICalendarDataStore
{
  public Task<CalendarEntity> CreateCalendarAsync(CalendarEntity entity, CancellationToken ct);
  public Task<CalendarEntity> UpdateCalendarAsync(CalendarEntity existingEntity, CalendarEntity updatedEntity, CancellationToken ct);
  public Task<CalendarEntity?> GetCalendarAsync(Guid id, CancellationToken ct);
  public IQueryable<CalendarEntity> GetAllCalendarsAsync(CancellationToken ct);
  public Task DeleteCalendarAsync(Guid id, CancellationToken ct);
}
