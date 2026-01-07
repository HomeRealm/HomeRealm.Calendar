using FamMan.Api.Calendars.Dtos.OccurrenceOverride;

namespace FamMan.Api.Calendars.Interfaces.OccurrenceOverride;

public interface IOccurrenceOverrideService
{
  public Task<OccurrenceOverrideResponseDto> CreateOccurrenceOverrideAsync(OccurrenceOverrideRequestDto dto, CancellationToken ct);
  public Task<(string status, OccurrenceOverrideResponseDto? updatedOccurrenceOverride)> UpdateOccurrenceOverrideAsync(OccurrenceOverrideRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string status, OccurrenceOverrideResponseDto? occurrenceOverride)> GetOccurrenceOverrideAsync(Guid id, CancellationToken ct);
  public Task<List<OccurrenceOverrideResponseDto>> GetAllOccurrenceOverridesAsync(CancellationToken ct);
  public Task DeleteOccurrenceOverrideAsync(Guid id, CancellationToken ct);
}
