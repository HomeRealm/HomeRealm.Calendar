using FamMan.Api.Calendars.Dtos.CalendarEvent;

namespace FamMan.Api.Calendars.Interfaces.CalendarEvent;

public interface ICalendarEventService
{

  public Task<CalendarEventResponseDto> CreateCalendarEventAsync(CalendarEventRequestDto dto, CancellationToken ct);
  public Task<(string, CalendarEventResponseDto?)> UpdateCalendarEventAsync(CalendarEventRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string, CalendarEventResponseDto?)> GetCalendarEventAsync(Guid id, CancellationToken ct);
  public Task<List<CalendarEventResponseDto>> GetAllCalendarEventsAsync(CancellationToken ct);
  public Task DeleteCalendarEventAsync(Guid id, CancellationToken ct);
}
