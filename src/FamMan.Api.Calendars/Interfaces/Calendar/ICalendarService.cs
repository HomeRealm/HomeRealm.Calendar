using FamMan.Api.Calendars.Dtos;

namespace FamMan.Api.Calendars.Interfaces.Calendar;

public interface ICalendarService
{
  public Task<CalendarResponseDto> CreateCalendarAsync(CalendarRequestDto dto, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? updatedCalendar)> UpdateCalendarAsync(CalendarRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? calendar)> GetCalendarAsync(Guid id, CancellationToken ct);
  public Task<List<CalendarResponseDto>> GetAllCalendarsAsync(CancellationToken ct);
  public Task DeleteCalendarAsync(Guid id, CancellationToken ct);
}
