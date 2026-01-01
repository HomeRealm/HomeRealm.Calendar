using System.ComponentModel;

namespace FamMan.Api.Calendars.Interfaces;

public interface ICalendarRequestDto
{
  [Description("Calendar Name")]
  public string Name { get; set; }
  [Description("Calendar Description")]
  public string Description { get; set; }
  [Description("Calendar Display Color")]
  public string Color { get; set; }
  [Description("Owner User ID")]
  public string Owner { get; set; }
  [Description("Public/Private/Shared")]
  public string Visibility { get; set; }
};

public interface ICalenderResponseDto : ICalendarRequestDto
{
  [Description("Unique Identifier")]
  public Guid Id { get; set; }
}