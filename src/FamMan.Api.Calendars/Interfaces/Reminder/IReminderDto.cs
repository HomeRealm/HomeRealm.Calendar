using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces.Reminder;

public interface IReminderRequestDto
{
  [Description("Event ID")]
  public Guid EventId { get; set; }
  [Description("Reminder Method")]
  public string Method { get; set; }
  [Description("Minutes Before Event")]
  public int TimeBefore { get; set; }
};

public interface IReminderResponseDto : IReminderRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}
