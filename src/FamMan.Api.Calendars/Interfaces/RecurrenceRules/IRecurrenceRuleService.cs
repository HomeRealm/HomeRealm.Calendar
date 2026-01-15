using FamMan.Api.Calendars.Dtos.RecurrenceRules;

namespace FamMan.Api.Calendars.Interfaces.RecurrenceRules;

public interface IRecurrenceRuleService
{
  public Task<RecurrenceRuleResponseDto> CreateRecurrenceRuleAsync(RecurrenceRuleDto dto, CancellationToken ct);
  public Task<(string status, RecurrenceRuleResponseDto? updatedRecurrenceRule)> UpdateRecurrenceRuleAsync(RecurrenceRuleDto dto, Guid id, CancellationToken ct);
  public Task<(string status, RecurrenceRuleResponseDto? recurrenceRule)> GetRecurrenceRuleAsync(Guid id, CancellationToken ct);
  public Task<(string status, RecurrenceRuleResponseDto? recurrenceRule)> GetRecurrenceRuleForCalendarEventAsync(Guid id, CancellationToken ct);
  public Task<List<RecurrenceRuleResponseDto>> GetAllRecurrenceRulesAsync(CancellationToken ct);
  public Task DeleteRecurrenceRuleAsync(Guid id, CancellationToken ct);
}

