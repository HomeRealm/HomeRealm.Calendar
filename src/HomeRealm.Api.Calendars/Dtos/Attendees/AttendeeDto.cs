namespace HomeRealm.Api.Calendars.Dtos.Attendees;

/// <summary>
/// Represents attendee details used when creating or updating an attendee for a calendar event.
/// </summary>
public record AttendeeDto
{
  /// <summary>
  /// Identifier of the calendar event this attendee is associated with.
  /// </summary>
  public required Guid EventId { get; init; }

  /// <summary>
  /// Identifier of the user participating as an attendee.
  /// </summary>
  public required Guid UserId { get; init; }

  /// <summary>
  /// Participation status for the attendee (for example, accepted or declined).
  /// </summary>
  public required string Status { get; init; }

  /// <summary>
  /// Role assigned to the attendee within the event (for example, organizer or participant).
  /// </summary>
  public required string Role { get; init; }
}
