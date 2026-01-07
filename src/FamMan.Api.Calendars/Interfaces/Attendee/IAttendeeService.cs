using FamMan.Api.Calendars.Dtos.Attendee;

namespace FamMan.Api.Calendars.Interfaces.Attendee;

public interface IAttendeeService
{
  public Task<AttendeeResponseDto> CreateAttendeeAsync(AttendeeRequestDto dto, CancellationToken ct);
  public Task<(string status, AttendeeResponseDto? updatedAttendee)> UpdateAttendeeAsync(AttendeeRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string status, AttendeeResponseDto? attendee)> GetAttendeeAsync(Guid id, CancellationToken ct);
  public Task<List<AttendeeResponseDto>> GetAllAttendeesAsync(CancellationToken ct);
  public Task DeleteAttendeeAsync(Guid id, CancellationToken ct);
}
