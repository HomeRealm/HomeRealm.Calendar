using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarDataStore
{
  public Task<CalendarEntity> CreateCalendarAsync(CalendarEntity entity, CancellationToken ct);
  public Task<CalendarEntity> UpdateCalendarAsync(CalendarEntity existingEntity, CalendarEntity updatedEntity, CancellationToken ct);
}
