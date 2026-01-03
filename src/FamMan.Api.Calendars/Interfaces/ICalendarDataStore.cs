using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarDataStore
{
  public Task<CalendarEntity> CreateEventAsync(CalendarEntity entity, CancellationToken ct);
}
