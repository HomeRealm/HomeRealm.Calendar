using FamMan.Api.Calendars.Dtos.RecurrenceRules;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.RecurrenceRules;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.RecurrenceRules;

public class RecurrenceRuleService : IRecurrenceRuleService
{
  private readonly IRecurrenceRuleDataStore _dataStore;
  public RecurrenceRuleService(IRecurrenceRuleDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<RecurrenceRuleResponseDto> CreateRecurrenceRuleAsync(RecurrenceRuleRequestDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateRecurrenceRuleAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, RecurrenceRuleResponseDto? updatedRecurrenceRule)> UpdateRecurrenceRuleAsync(RecurrenceRuleRequestDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetRecurrenceRuleAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateRecurrenceRuleAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, RecurrenceRuleResponseDto? recurrenceRule)> GetRecurrenceRuleAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetRecurrenceRuleAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<RecurrenceRuleResponseDto>> GetAllRecurrenceRulesAsync(CancellationToken ct)
  {
    var recurrenceRules = _dataStore.GetAllRecurrenceRulesAsync(ct);

    var mappedRecurrenceRules = await recurrenceRules.Select(recurrenceRule => MapToResponseDto(recurrenceRule)).ToListAsync(ct);
    return mappedRecurrenceRules;
  }
  public async Task DeleteRecurrenceRuleAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteRecurrenceRuleAsync(id, ct);
  }
  private RecurrenceRuleEntity MapToEntity(RecurrenceRuleRequestDto dto, Guid? id = null)
  {
    return new RecurrenceRuleEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      EventId = dto.EventId,
      Rule = dto.Rule,
      OccurrenceOverrides = dto.OccurrenceOverrides,
      EndDate = dto.EndDate
    };
  }
  private RecurrenceRuleResponseDto MapToResponseDto(RecurrenceRuleEntity entity)
  {
    return new RecurrenceRuleResponseDto
    {
      Id = entity.Id,
      EventId = entity.EventId,
      Rule = entity.Rule,
      OccurrenceOverrides = entity.OccurrenceOverrides,
      EndDate = entity.EndDate
    };
  }
}
