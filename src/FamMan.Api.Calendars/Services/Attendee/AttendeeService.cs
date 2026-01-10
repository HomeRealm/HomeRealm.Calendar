using FamMan.Api.Calendars.Dtos.Attendee;
using FamMan.Api.Calendars.Interfaces.Attendee;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars.Services.Attendee;

public class AttendeeService : IAttendeeService
{
  private readonly IAttendeeDataStore _dataStore;
  public AttendeeService(IAttendeeDataStore dataStore)
  {
    _dataStore = dataStore;
  }
  public async Task<AttendeeResponseDto> CreateAttendeeAsync(AttendeeRequestDto dto, CancellationToken ct)
  {
    var mappedEntity = MapToEntity(dto);
    var savedEntity = await _dataStore.CreateAttendeeAsync(mappedEntity, ct);
    return MapToResponseDto(savedEntity);
  }
  public async Task<(string status, AttendeeResponseDto? updatedAttendee)> UpdateAttendeeAsync(AttendeeRequestDto dto, Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetAttendeeAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    var mappedEntity = MapToEntity(dto, id);
    var updatedEntity = await _dataStore.UpdateAttendeeAsync(existingEntity, mappedEntity, ct);

    return ("updated", MapToResponseDto(updatedEntity));
  }
  public async Task<(string status, AttendeeResponseDto? attendee)> GetAttendeeAsync(Guid id, CancellationToken ct)
  {
    var existingEntity = await _dataStore.GetAttendeeAsync(id, ct);
    if (existingEntity is null)
    {
      return ("notfound", null);
    }

    return ("found", MapToResponseDto(existingEntity));
  }
  public async Task<List<AttendeeResponseDto>> GetAllAttendeesAsync(CancellationToken ct)
  {
    var attendees = _dataStore.GetAllAttendeesAsync(ct);

    var mappedAttendees = await attendees.Select(attendee => MapToResponseDto(attendee)).ToListAsync(ct);
    return mappedAttendees;
  }
  public async Task DeleteAttendeeAsync(Guid id, CancellationToken ct)
  {
    await _dataStore.DeleteAttendeeAsync(id, ct);
  }
  private Entities.AttendeeEntity MapToEntity(AttendeeRequestDto dto, Guid? id = null)
  {
    return new Entities.AttendeeEntity
    {
      Id = id ?? Guid.CreateVersion7(),
      EventId = dto.EventId,
      UserId = dto.UserId,
      Status = dto.Status,
      Role = dto.Role
    };
  }
  private AttendeeResponseDto MapToResponseDto(Entities.AttendeeEntity entity)
  {
    return new AttendeeResponseDto
    {
      Id = entity.Id,
      EventId = entity.EventId,
      UserId = entity.UserId,
      Status = entity.Status,
      Role = entity.Role
    };
  }
}
