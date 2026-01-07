using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.RecurrenceRule;

public interface IRecurrenceRuleDataStore
{
  public Task<RecurrenceRuleEntity> CreateRecurrenceRuleAsync(RecurrenceRuleEntity entity, CancellationToken ct);
  public Task<RecurrenceRuleEntity> UpdateRecurrenceRuleAsync(RecurrenceRuleEntity existingEntity, RecurrenceRuleEntity updatedEntity, CancellationToken ct);
  public Task<RecurrenceRuleEntity?> GetRecurrenceRuleAsync(Guid id, CancellationToken ct);
  public IQueryable<RecurrenceRuleEntity> GetAllRecurrenceRulesAsync(CancellationToken ct);
  public Task DeleteRecurrenceRuleAsync(Guid id, CancellationToken ct);
}
