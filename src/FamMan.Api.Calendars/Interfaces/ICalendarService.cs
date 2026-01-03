using FamMan.Api.Calendars.Dtos;
using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarService
{
  public Task<CalendarResponseDto> CreateCalendarAsync(CalendarRequestDto dto, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? updatedCalendar)> UpdateCalendarAsync(CalendarRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? calendar)> GetCalendarAsync(Guid id, CancellationToken ct);
}
