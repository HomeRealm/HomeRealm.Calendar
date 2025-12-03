using System.Text.Json;
using FamMan.Api.Events.Dtos;
using FamMan.Api.Events.Entities;
using FamMan.Api.Events.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Events.Services;

public class EventService(IEventDataStore dataStore) : IEventService
{
    private readonly IEventDataStore _dataStore = dataStore;
    public async Task<ActionableEventDto> CreateEventAsync(ActionableEventDto actionableEventDto, CancellationToken ct)
    {
        var mappedEvent = MapToEntity(actionableEventDto);
        var createdEvent = await _dataStore.CreateEventAsync(mappedEvent, ct);
        return MapToDto(createdEvent);
    }
    public async Task<(string status, ActionableEventDto? actionableEventDto)> UpdateEventAsync(ActionableEventDto actionableEventDto, CancellationToken ct)
    {
        var existingEvent = await _dataStore.GetEventByIdAsync(actionableEventDto.Id, ct);
        if (existingEvent is null)
        {
            return ("notfound", null);
        }
        var modifiedEvent = MapToEntity(actionableEventDto, update: true);

        var updatedEvent = await _dataStore.UpdateEventAsync(existingEvent, modifiedEvent, ct);
        return ("updated", MapToDto(updatedEvent));
    }
    public async Task<List<ActionableEventDto>> GetAllEventsAsync(CancellationToken ct)
    {
        var actionableEvents = _dataStore.GetAllEvents();

        var mappedEvents = await actionableEvents.Select(
          actionableEvent => new ActionableEventDto
          {
              Id = actionableEvent.Id,
              Name = actionableEvent.Name,
              EventType = actionableEvent.EventType,
              Description = actionableEvent.Description,
              RecurrenceRules = JsonSerializer.Deserialize<JsonElement>(actionableEvent.RecurrenceRules),
              IsActive = actionableEvent.IsActive,
              CreatedAt = actionableEvent.CreatedAt,
              UpdatedAt = actionableEvent.UpdatedAt
          }
        ).ToListAsync(ct);

        return mappedEvents;
    }
    public async Task<(string status, ActionableEventDto? actionableEventDto)> GetEventByIdAsync(Guid id, CancellationToken ct)
    {
        var existingEvent = await _dataStore.GetEventByIdAsync(id, ct);

        if (existingEvent is null)
        {
            return ("notfound", null);
        }

        var mappedEvent = MapToDto(existingEvent);

        return ("found", mappedEvent);
    }
    public async Task DeleteEventAsync(Guid id, CancellationToken ct)
    {
        await _dataStore.DeleteEventAsync(id, ct);
    }
    private ActionableEventDto MapToDto(ActionableEvent actionableEvent)
    {
        return new ActionableEventDto
        {
            Id = actionableEvent.Id,
            Name = actionableEvent.Name,
            EventType = actionableEvent.EventType,
            Description = actionableEvent.Description,
            RecurrenceRules = JsonSerializer.Deserialize<JsonElement>(actionableEvent.RecurrenceRules),
            IsActive = actionableEvent.IsActive,
            CreatedAt = actionableEvent.CreatedAt,
            UpdatedAt = actionableEvent.UpdatedAt
        };
    }
    private ActionableEvent MapToEntity(ActionableEventDto actionableEventDto, bool update = false)
    {
        return new ActionableEvent
        {
            Id = actionableEventDto.Id,
            Name = actionableEventDto.Name,
            EventType = actionableEventDto.EventType,
            Description = actionableEventDto.Description,
            RecurrenceRules = JsonSerializer.Serialize(actionableEventDto.RecurrenceRules),
            IsActive = actionableEventDto.IsActive,
            CreatedAt = actionableEventDto.CreatedAt,
            UpdatedAt = update ? DateTime.Now : actionableEventDto.UpdatedAt
        };
    }
}
