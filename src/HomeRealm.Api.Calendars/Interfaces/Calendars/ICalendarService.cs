using HomeRealm.Api.Calendars.Dtos.Calendars;

namespace HomeRealm.Api.Calendars.Interfaces.Calendars;

public interface ICalendarService
{
  public Task<CalendarResponseDto> CreateCalendarAsync(CalendarDto dto, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? updatedCalendar)> UpdateCalendarAsync(CalendarDto dto, Guid id, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? calendar)> GetCalendarAsync(Guid id, CancellationToken ct);
  public Task<List<CalendarResponseDto>> GetAllCalendarsAsync(CancellationToken ct);
  public Task DeleteCalendarAsync(Guid id, CancellationToken ct);
}

