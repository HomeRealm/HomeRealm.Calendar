using FamMan.Api.Calendars.Entities;

namespace FamMan.Api.Calendars.Interfaces.Reminders;

public interface IReminderDataStore
{
  public Task<ReminderEntity> CreateReminderAsync(ReminderEntity entity, CancellationToken ct);
  public Task<ReminderEntity> UpdateReminderAsync(ReminderEntity existingEntity, ReminderEntity updatedEntity, CancellationToken ct);
  public Task<ReminderEntity?> GetReminderAsync(Guid id, CancellationToken ct);
  public IQueryable<ReminderEntity> GetRemindersForCalendarEvent(Guid id);
  public IQueryable<ReminderEntity> GetAllReminders();
  public Task DeleteReminderAsync(Guid id, CancellationToken ct);
}
