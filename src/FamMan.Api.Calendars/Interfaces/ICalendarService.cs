using FamMan.Api.Calendars.Dtos;
using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarService
{
  public Task<CalendarResponseDto> CreateCalendarAsync(CalendarRequestDto dto, CancellationToken ct);
}
