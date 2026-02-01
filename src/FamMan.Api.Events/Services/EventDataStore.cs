using FamMan.Api.Events.Entities;
using FamMan.Api.Events.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Events.Services;

public class EventDataStore(EventsDbContext db) : IEventDataStore
{
  private readonly EventsDbContext _db = db;
  public async Task<ActionableEvent> CreateEventAsync(ActionableEvent actionableEvent, CancellationToken ct)
  {
    _db.ActionableEvents.Add(actionableEvent);
    await _db.SaveChangesAsync(ct);
    return actionableEvent;
  }
  public async Task<ActionableEvent> UpdateEventAsync(ActionableEvent existingEvent, ActionableEvent updatedEvent, CancellationToken ct)
  {
    _db.Entry(existingEvent).CurrentValues.SetValues(updatedEvent);
    await _db.SaveChangesAsync(ct);
    return existingEvent;
  }
  public IQueryable<ActionableEvent> GetAllEvents()
  {
    return _db.ActionableEvents.AsNoTracking().AsQueryable();
  }
  public async Task<ActionableEvent?> GetEventByIdAsync(Guid id, CancellationToken ct)
  {
    return await _db.ActionableEvents.FindAsync(id, ct);
  }
  public async Task DeleteEventAsync(Guid id, CancellationToken ct)
  {
    await _db.ActionableEvents.Where(e => e.Id == id).ExecuteDeleteAsync(ct);
  }
}
