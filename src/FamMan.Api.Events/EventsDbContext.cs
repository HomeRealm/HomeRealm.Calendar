using FamMan.Api.Events.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Events
{
    public class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options)
    {
        public DbSet<ActionableEvent> ActionableEvents { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // automatically apply all configurations from the current assembly
            // Configuration classer are located in the EntityConfiguration directory. 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
        }
    }
}
