namespace FamMan.Api.Calendars.Dtos.RecurrenceRules;

public record RecurrenceRuleResponseDto : RecurrenceRuleDto
{
  public Guid Id { get; set; }
}
