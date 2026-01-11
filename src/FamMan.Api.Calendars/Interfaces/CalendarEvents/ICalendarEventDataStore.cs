using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.CalendarEvents;

public interface ICalendarEventDataStore
{
  public Task<CalendarEventEntity> CreateCalendarEventAsync(CalendarEventEntity entity, CancellationToken ct);
  public Task<CalendarEventEntity> UpdateCalendarEventAsync(CalendarEventEntity existingEntity, CalendarEventEntity updatedEntity, CancellationToken ct);
  public Task<CalendarEventEntity?> GetCalendarEventAsync(Guid id, CancellationToken ct);
  public IQueryable<CalendarEventEntity> GetCalendarEventsForCalendarAsync(Guid calendarId, CancellationToken ct);
  public IQueryable<CalendarEventEntity> GetAllCalendarEventsAsync(CancellationToken ct);
  public Task DeleteCalendarEventAsync(Guid id, CancellationToken ct);
}
