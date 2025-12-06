using FamMan.Api.Chores.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Chores.Services;

/// <inheritdoc/>
public class ChoreDataStore(ChoresDbContext db) : IChoreDataStore
{
  /// <inheritdoc/>
  public IQueryable<Chore> GetChores() => db.Chores.AsNoTracking();

  /// <inheritdoc/>
  public async Task<Chore?> GetChoreAsync(Guid choreId, CancellationToken ct) => await db.Chores.FindAsync(choreId, ct);

  /// <inheritdoc/>
  public Task DeleteChoreAsync(Guid choreId, CancellationToken ct) => db.Chores.Where(c => c.Id == choreId).ExecuteDeleteAsync(ct);

  /// <inheritdoc/>
  public async Task<Chore> CreateChoreAsync(Chore chore, CancellationToken ct)
  {
    db.Chores.Add(chore);
    await db.SaveChangesAsync(ct);
    return chore;
  }

  /// <inheritdoc/>
  public async Task<Chore> UpdateChoreAsync(Chore existingChore, Chore updatedChore, CancellationToken ct)
  {
    db.Entry(existingChore).CurrentValues.SetValues(updatedChore);
    await db.SaveChangesAsync(ct);
    return existingChore;
  }
}
