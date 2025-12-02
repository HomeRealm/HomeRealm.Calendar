using System;
using FamMan.Api.Events.Dtos;

namespace FamMan.Api.Events.Interfaces;

public interface IEventService
{
  public Task<ActionableEventDto> CreateEventAsync(ActionableEventDto actionableEventDto, CancellationToken ct);
  public Task<(string status, ActionableEventDto? actionableEventDto)> UpdateEventAsync(ActionableEventDto actionableEvent, CancellationToken ct);
  public Task<List<ActionableEventDto>> GetAllEventsAsync(CancellationToken ct);
  public Task<(string status, ActionableEventDto? actionableEventDto)> GetEventByIdAsync(Guid id, CancellationToken ct);
  public Task DeleteEventAsync(Guid id, CancellationToken ct);
}
