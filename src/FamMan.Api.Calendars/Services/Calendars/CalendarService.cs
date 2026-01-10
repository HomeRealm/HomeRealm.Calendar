using FamMan.Api.Calendars.Dtos.Calendars;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Calendars;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Calendars;

public class CalendarService : ICalendarService
{
  private readonly ICalendarDataStore _dataStore;
  public CalendarService(ICalendarDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<CalendarResponseDto> CreateCalendarAsync(CalendarRequestDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateCalendarAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, CalendarResponseDto? updatedCalendar)> UpdateCalendarAsync(CalendarRequestDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetCalendarAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateCalendarAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, CalendarResponseDto? calendar)> GetCalendarAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetCalendarAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<CalendarResponseDto>> GetAllCalendarsAsync(CancellationToken ct)
  {
    var calendars = _dataStore.GetAllCalendarsAsync(ct);

    var mappedCalendars = await calendars.Select(calendar => MapToResponseDto(calendar)).ToListAsync(ct);
    return mappedCalendars;
  }
  public async Task DeleteCalendarAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteCalendarAsync(id, ct);
  }
  private CalendarEntity MapToEntity(CalendarRequestDto dto, Guid? id = null)
  {
    return new CalendarEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      Name = dto.Name,
      Description = dto.Description,
      Color = dto.Color,
      Owner = dto.Owner,
      Visibility = dto.Visibility
    };
  }
  private CalendarResponseDto MapToResponseDto(CalendarEntity entity)
  {
    return new CalendarResponseDto
    {
      Id = entity.Id,
      Name = entity.Name,
      Description = entity.Description,
      Color = entity.Color,
      Owner = entity.Owner,
      Visibility = entity.Visibility
    };
  }
}
