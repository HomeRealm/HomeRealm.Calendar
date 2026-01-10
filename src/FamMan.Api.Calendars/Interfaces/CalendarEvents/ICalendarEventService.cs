using FamMan.Api.Calendars.Dtos.CalendarEvents;

namespace FamMan.Api.Calendars.Interfaces.CalendarEvents;

public interface ICalendarEventService
{

  public Task<CalendarEventResponseDto> CreateCalendarEventAsync(CalendarEventDto dto, CancellationToken ct);
  public Task<(string status, CalendarEventResponseDto? updatedCalendarEvent)> UpdateCalendarEventAsync(CalendarEventDto dto, Guid id, CancellationToken ct);
  public Task<(string status, CalendarEventResponseDto? calendarEvent)> GetCalendarEventAsync(Guid id, CancellationToken ct);
  public Task<List<CalendarEventResponseDto>> GetAllCalendarEventsAsync(CancellationToken ct);
  public Task DeleteCalendarEventAsync(Guid id, CancellationToken ct);
}

