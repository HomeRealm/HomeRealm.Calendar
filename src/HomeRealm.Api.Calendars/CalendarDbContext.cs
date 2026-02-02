using HomeRealm.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeRealm.Api.Calendars;

public class CalendarDbContext(DbContextOptions<CalendarDbContext> options) : DbContext(options)
{
  public DbSet<CalendarEntity> Calendars { get; set; } = default!;
  public DbSet<AttendeeEntity> Attendees { get; set; } = default!;
  public DbSet<CalendarEventEntity> CalendarEvents { get; set; } = default!;
  public DbSet<CategoryEntity> Categories { get; set; } = default!;
  public DbSet<OccurrenceOverrideEntity> OccurrenceOverrides { get; set; } = default!;
  public DbSet<RecurrenceRuleEntity> RecurrenceRules { get; set; } = default!;
  public DbSet<ReminderEntity> Reminders { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // automatically apply all configurations from the current assembly
    // Configuration classes are located in the EntityConfiguration directory. 
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalendarDbContext).Assembly);
  }
}

