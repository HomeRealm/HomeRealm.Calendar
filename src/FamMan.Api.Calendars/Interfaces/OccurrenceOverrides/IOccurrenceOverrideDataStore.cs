using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;

public interface IOccurrenceOverrideDataStore
{
  public Task<OccurrenceOverrideEntity> CreateOccurrenceOverrideAsync(OccurrenceOverrideEntity entity, CancellationToken ct);
  public Task<OccurrenceOverrideEntity> UpdateOccurrenceOverrideAsync(OccurrenceOverrideEntity existingEntity, OccurrenceOverrideEntity updatedEntity, CancellationToken ct);
  public Task<OccurrenceOverrideEntity?> GetOccurrenceOverrideAsync(Guid id, CancellationToken ct);
  public IQueryable<OccurrenceOverrideEntity> GetAllOccurrenceOverridesAsync(CancellationToken ct);
  public Task DeleteOccurrenceOverrideAsync(Guid id, CancellationToken ct);
}
