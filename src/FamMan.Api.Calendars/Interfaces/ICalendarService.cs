using FamMan.Api.Calendars.Dtos;
using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarService
{
  public Task<CalendarResponseDto> CreateCalendarAsync(CalendarEntity entity, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? dto)> UpdateCalendarAsync(CalendarEntity entity, CancellationToken ct);
  public Task<(string status, CalendarResponseDto? dto)> GetCalendarAsync(Guid id, CancellationToken ct);
  public Task DeleteCalendarAsync(Guid id, CancellationToken ct);
}
