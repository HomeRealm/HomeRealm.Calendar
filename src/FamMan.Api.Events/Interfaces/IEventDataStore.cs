using FamMan.Api.Events.Entities;

namespace FamMan.Api.Events.Interfaces;

public interface IEventDataStore
{
  public Task<ActionableEvent> CreateEventAsync(ActionableEvent actionableEvent, CancellationToken ct);
  public Task<ActionableEvent> UpdateEventAsync(ActionableEvent existingEvent, ActionableEvent updatedEvent, CancellationToken ct);
  public IQueryable<ActionableEvent> GetAllEvents();
  public Task<ActionableEvent?> GetEventByIdAsync(Guid id, CancellationToken ct);
  public Task DeleteEventAsync(Guid id, CancellationToken ct);
}
