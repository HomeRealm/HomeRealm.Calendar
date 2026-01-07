using FamMan.Api.Calendars.Dtos.RecurrenceRule;

namespace FamMan.Api.Calendars.Interfaces.RecurrenceRule;

public interface IRecurrenceRuleService
{
  public Task<RecurrenceRuleResponseDto> CreateRecurrenceRuleAsync(RecurrenceRuleRequestDto dto, CancellationToken ct);
  public Task<(string status, RecurrenceRuleResponseDto? updatedRecurrenceRule)> UpdateRecurrenceRuleAsync(RecurrenceRuleRequestDto dto, Guid id, CancellationToken ct);
  public Task<(string status, RecurrenceRuleResponseDto? recurrenceRule)> GetRecurrenceRuleAsync(Guid id, CancellationToken ct);
  public Task<List<RecurrenceRuleResponseDto>> GetAllRecurrenceRulesAsync(CancellationToken ct);
  public Task DeleteRecurrenceRuleAsync(Guid id, CancellationToken ct);
}
