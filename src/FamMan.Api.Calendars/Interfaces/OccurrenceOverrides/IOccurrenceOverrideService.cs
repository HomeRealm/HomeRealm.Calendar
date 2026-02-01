using FamMan.Api.Calendars.Dtos.OccurrenceOverrides;

namespace FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;

public interface IOccurrenceOverrideService
{
  public Task<OccurrenceOverrideResponseDto> CreateOccurrenceOverrideAsync(OccurrenceOverrideDto dto, CancellationToken ct);
  public Task<(string status, OccurrenceOverrideResponseDto? updatedOccurrenceOverride)> UpdateOccurrenceOverrideAsync(OccurrenceOverrideDto dto, Guid id, CancellationToken ct);
  public Task<(string status, OccurrenceOverrideResponseDto? occurrenceOverride)> GetOccurrenceOverrideAsync(Guid id, CancellationToken ct);
  public Task<List<OccurrenceOverrideResponseDto>> GetOccurrenceOverridesForRecurrenceRuleAsync(Guid id, CancellationToken ct);
  public Task<List<OccurrenceOverrideResponseDto>> GetAllOccurrenceOverridesAsync(CancellationToken ct);
  public Task DeleteOccurrenceOverrideAsync(Guid id, CancellationToken ct);
}

