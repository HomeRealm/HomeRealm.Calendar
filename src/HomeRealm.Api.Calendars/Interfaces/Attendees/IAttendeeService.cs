using HomeRealm.Api.Calendars.Dtos.Attendees;

namespace HomeRealm.Api.Calendars.Interfaces.Attendees;

public interface IAttendeeService
{
  public Task<AttendeeResponseDto> CreateAttendeeAsync(AttendeeDto dto, CancellationToken ct);
  public Task<(string status, AttendeeResponseDto? updatedAttendee)> UpdateAttendeeAsync(AttendeeDto dto, Guid id, CancellationToken ct);
  public Task<(string status, AttendeeResponseDto? attendee)> GetAttendeeAsync(Guid id, CancellationToken ct);
  public Task<List<AttendeeResponseDto>> GetAttendeesForCalendarEventAsync(Guid id, CancellationToken ct);
  public Task<List<AttendeeResponseDto>> GetAllAttendeesAsync(CancellationToken ct);
  public Task DeleteAttendeeAsync(Guid id, CancellationToken ct);
}


