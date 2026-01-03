using FamMan.Api.Calendars.Dtos;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces;

namespace FamMan.Api.Calendars.Services;

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
