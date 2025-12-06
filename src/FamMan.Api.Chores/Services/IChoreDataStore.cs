using FamMan.Api.Chores.Entities;

namespace FamMan.Api.Chores.Services;

/// <summary>
/// Defines the contract for a data store that manages chore entities, providing methods to create, retrieve, update,
/// and delete chores.
/// </summary>
/// <remarks>Implementations of this interface are responsible for persisting and retrieving chore data. Methods
/// may be asynchronous and can interact with various storage backends, such as databases or in-memory collections.
/// Thread safety and transaction support depend on the specific implementation.</remarks>
public interface IChoreDataStore
{
  /// <summary>
  /// Creates a new chore asynchronously and returns the created chore with any server-assigned properties.
  /// </summary>
  /// <param name="chore">The chore to create. Must not be null. Properties such as name and due date should be set as required by the
  /// system.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the created chore, including any
  /// properties assigned by the server.</returns>
  Task<Chore> CreateChoreAsync(Chore chore, CancellationToken ct);

  /// <summary>
  /// Deletes the chore identified by the specified unique identifier.
  /// </summary>
  /// <param name="choreId">The unique identifier of the chore to delete.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous delete operation.</returns>
  Task DeleteChoreAsync(Guid choreId, CancellationToken ct);

  /// <summary>
  /// Retrieves the chore associated with the specified unique identifier.
  /// </summary>
  /// <param name="choreId">The unique identifier of the chore to retrieve.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the chore if found; otherwise, <see
  /// langword="null"/>.</returns>
  Task<Chore?> GetChoreAsync(Guid choreId, CancellationToken ct);

  /// <summary>
  /// Retrieves a queryable collection of chores from the data source.
  /// </summary>
  /// <remarks>The returned query allows for deferred execution and can be further filtered or projected
  /// using LINQ methods before enumeration. Changes to the underlying data source after obtaining the query may
  /// affect the results when the query is executed.</remarks>
  /// <returns>An <see cref="IQueryable{Chore}"/> representing the chores available in the data source. The query is not
  /// executed until the collection is enumerated.</returns>
  IQueryable<Chore> GetChores();

  /// <summary>
  /// Updates the specified chore with new values and returns the updated chore.
  /// </summary>
  /// <param name="existingChore">The chore to be updated. Cannot be null.</param>
  /// <param name="updatedChore">An object containing the new values to apply to the existing chore. Cannot be null.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the updated chore.</returns>
  Task<Chore> UpdateChoreAsync(Chore existingChore, Chore updatedChore, CancellationToken ct);
}