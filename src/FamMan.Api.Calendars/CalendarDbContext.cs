using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Calendars;

public class CalendarDbContext(DbContextOptions<CalendarDbContext> options) : DbContext(options)
{
  public DbSet<CalendarEntity> Calendars { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // automatically apply all configurations from the current assembly
    // Configuration classes are located in the EntityConfiguration directory. 
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalendarDbContext).Assembly);
  }
}
