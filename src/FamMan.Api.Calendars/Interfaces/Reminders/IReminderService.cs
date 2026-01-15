using FamMan.Api.Calendars.Dtos.Reminders;

namespace FamMan.Api.Calendars.Interfaces.Reminders;

public interface IReminderService
{
  public Task<ReminderResponseDto> CreateReminderAsync(ReminderDto dto, CancellationToken ct);
  public Task<(string status, ReminderResponseDto? updatedReminder)> UpdateReminderAsync(ReminderDto dto, Guid id, CancellationToken ct);
  public Task<(string status, ReminderResponseDto? reminder)> GetReminderAsync(Guid id, CancellationToken ct);
  public Task<List<ReminderResponseDto>> GetRemindersForCalendarEventAsync(Guid id, CancellationToken ct);
  public Task<List<ReminderResponseDto>> GetAllRemindersAsync(CancellationToken ct);
  public Task DeleteReminderAsync(Guid id, CancellationToken ct);
}

