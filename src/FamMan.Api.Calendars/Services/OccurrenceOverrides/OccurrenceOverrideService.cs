using FamMan.Api.Calendars.Dtos.OccurrenceOverrides;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.OccurrenceOverrides;

public class OccurrenceOverrideService : IOccurrenceOverrideService
{
  private readonly IOccurrenceOverrideDataStore _dataStore;
  public OccurrenceOverrideService(IOccurrenceOverrideDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<OccurrenceOverrideResponseDto> CreateOccurrenceOverrideAsync(OccurrenceOverrideDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateOccurrenceOverrideAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, OccurrenceOverrideResponseDto? updatedOccurrenceOverride)> UpdateOccurrenceOverrideAsync(OccurrenceOverrideDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetOccurrenceOverrideAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateOccurrenceOverrideAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, OccurrenceOverrideResponseDto? occurrenceOverride)> GetOccurrenceOverrideAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetOccurrenceOverrideAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<OccurrenceOverrideResponseDto>> GetOccurrenceOverridesForRecurrenceRuleAsync(Guid id, CancellationToken ct)
  {
    var occurrenceOverrides = _dataStore.GetOccurrenceOverridesForRecurrenceRule(id);

    var mappedOccurrenceOverrides = await occurrenceOverrides.Select(occurrenceOverride => MapToResponseDto(occurrenceOverride)).ToListAsync(ct);
    return mappedOccurrenceOverrides;
  }
  public async Task<List<OccurrenceOverrideResponseDto>> GetAllOccurrenceOverridesAsync(CancellationToken ct)
  {
    var occurrenceOverrides = _dataStore.GetAllOccurrenceOverrides();

    var mappedOccurrenceOverrides = await occurrenceOverrides.Select(occurrenceOverride => MapToResponseDto(occurrenceOverride)).ToListAsync(ct);
    return mappedOccurrenceOverrides;
  }
  public async Task DeleteOccurrenceOverrideAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteOccurrenceOverrideAsync(id, ct);
  }
  private static OccurrenceOverrideEntity MapToEntity(OccurrenceOverrideDto dto, Guid? id = null)
  {
    return new OccurrenceOverrideEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      RecurrenceId = dto.RecurrenceId,
      Date = dto.Date
    };
  }
  private static OccurrenceOverrideResponseDto MapToResponseDto(OccurrenceOverrideEntity entity)
  {
    return new OccurrenceOverrideResponseDto
    {
      Id = entity.Id,
      RecurrenceId = entity.RecurrenceId,
      Date = entity.Date
    };
  }
}

