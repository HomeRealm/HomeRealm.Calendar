using FamMan.Api.Calendars.Dtos.CalendarEvents;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.CalendarEvents;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.CalendarEvents;

public class CalendarEventService : ICalendarEventService
{
  private readonly ICalendarEventDataStore _dataStore;
  public CalendarEventService(ICalendarEventDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<CalendarEventResponseDto> CreateCalendarEventAsync(CalendarEventDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateCalendarEventAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, CalendarEventResponseDto? updatedCalendarEvent)> UpdateCalendarEventAsync(CalendarEventDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetCalendarEventAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateCalendarEventAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, CalendarEventResponseDto? calendarEvent)> GetCalendarEventAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetCalendarEventAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<CalendarEventResponseDto>> GetCalendarEventsForCalendarAsync(Guid calendarId, CancellationToken ct)
  {
    var calendarEvents = _dataStore.GetCalendarEventsForCalendarAsync(calendarId, ct);

    var mappedCalendarEvents = await calendarEvents.Select(calendarEvent => MapToResponseDto(calendarEvent)).ToListAsync(ct);
    return mappedCalendarEvents;
  }
  public async Task<List<CalendarEventResponseDto>> GetAllCalendarEventsAsync(CancellationToken ct)
  {
    var calendarEvents = _dataStore.GetAllCalendarEventsAsync(ct);

    var mappedCalendarEvents = await calendarEvents.Select(calendarEvent => MapToResponseDto(calendarEvent)).ToListAsync(ct);
    return mappedCalendarEvents;
  }
  public async Task DeleteCalendarEventAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteCalendarEventAsync(id, ct);
  }
  private CalendarEventEntity MapToEntity(CalendarEventDto dto, Guid? id = null)
  {
    return new CalendarEventEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      CalendarId = dto.CalendarId,
      Title = dto.Title,
      Description = dto.Description,
      Start = dto.Start,
      End = dto.End,
      Location = dto.Location,
      AllDay = dto.AllDay,
      RecurrenceId = dto.RecurrenceId,
      CategoryId = dto.CategoryId,
      LinkedResource = dto.LinkedResource
    };
  }
  private CalendarEventResponseDto MapToResponseDto(CalendarEventEntity entity)
  {
    return new CalendarEventResponseDto
    {
      Id = entity.Id,
      CalendarId = entity.CalendarId,
      Title = entity.Title,
      Description = entity.Description,
      Start = entity.Start,
      End = entity.End,
      Location = entity.Location,
      AllDay = entity.AllDay,
      RecurrenceId = entity.RecurrenceId,
      CategoryId = entity.CategoryId,
      LinkedResource = entity.LinkedResource
    };
  }
}

