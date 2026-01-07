using FamMan.Api.Calendars.Dtos.Reminder;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Reminder;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Reminder;

public class ReminderService : IReminderService
{
  private readonly IReminderDataStore _dataStore;
  public ReminderService(IReminderDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<ReminderResponseDto> CreateReminderAsync(ReminderRequestDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateReminderAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, ReminderResponseDto? updatedReminder)> UpdateReminderAsync(ReminderRequestDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetReminderAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateReminderAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, ReminderResponseDto? reminder)> GetReminderAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetReminderAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<ReminderResponseDto>> GetAllRemindersAsync(CancellationToken ct)
  {
    var reminders = _dataStore.GetAllRemindersAsync(ct);

    var mappedReminders = await reminders.Select(reminder => MapToResponseDto(reminder)).ToListAsync(ct);
    return mappedReminders;
  }
  public async Task DeleteReminderAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteReminderAsync(id, ct);
  }
  private ReminderEntity MapToEntity(ReminderRequestDto dto, Guid? id = null)
  {
    return new ReminderEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      EventId = dto.EventId,
      Method = dto.Method,
      TimeBefore = dto.TimeBefore
    };
  }
  private ReminderResponseDto MapToResponseDto(ReminderEntity entity)
  {
    return new ReminderResponseDto
    {
      Id = entity.Id,
      EventId = entity.EventId,
      Method = entity.Method,
      TimeBefore = entity.TimeBefore
    };
  }
}
