using FamMan.Api.Chores.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Chores;

public class ChoresDbContext(DbContextOptions<ChoresDbContext> options) : DbContext(options)
{
    public DbSet<Chore> Chores { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // automatically apply all configurations from the current assembly
        // Configuration classer are located in the EntityConfiguration directory. 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChoresDbContext).Assembly);
    }
}
