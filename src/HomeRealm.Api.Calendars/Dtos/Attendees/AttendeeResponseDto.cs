namespace HomeRealm.Api.Calendars.Dtos.Attendees;

/// <inheritdoc />
public record AttendeeResponseDto : AttendeeDto
{
  /// <summary>
  /// Unique identifier for the attendee record.
  /// </summary>
  public Guid Id { get; init; }
}
