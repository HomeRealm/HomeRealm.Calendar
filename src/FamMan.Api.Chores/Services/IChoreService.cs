using FamMan.Api.Chores.Dtos;

namespace FamMan.Api.Chores.Services;

/// <summary>
/// Service for managing chores.
/// </summary>
public interface IChoreService
{
  /// <summary>
  /// Creates a new chore asynchronously using the specified chore details.
  /// </summary>
  /// <param name="choreDto">An object containing the details of the chore to create. Must not be null.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous operation. The task result contains a ChoreDto representing the created
  /// chore, including any server-assigned identifiers or properties.</returns>
  Task<ChoreDto> CreateChoreAsync(ChoreDto choreDto, CancellationToken ct);
  /// <summary>
  /// Deletes the chore identified by the specified unique identifier.
  /// </summary>
  /// <param name="choreId">The unique identifier of the chore to delete. Must correspond to an existing chore.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous delete operation.</returns>
  Task DeleteChoreAsync(Guid choreId, CancellationToken ct);
  /// <summary>
  /// Asynchronously retrieves the details of a chore identified by the specified unique identifier.
  /// </summary>
  /// <param name="choreId">The unique identifier of the chore to retrieve.</param>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ChoreDto"/> with the
  /// details of the chore if found; otherwise, <see langword="null"/>.</returns>
  Task<(string status, ChoreDto? chore)> GetChoreAsync(Guid choreId, CancellationToken ct);
  /// <summary>
  /// Asynchronously retrieves a list of chores.
  /// </summary>
  /// <param name="ct" </param>
  /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ChoreDto"/>
  /// objects representing the available chores. If no chores are found, the list will be empty.</returns>
  Task<List<ChoreDto>> GetChoresAsync(CancellationToken ct);
  /// <summary>
  /// Asynchronously updates the specified chore with new details and returns the update status and the updated chore
  /// information.
  /// </summary>
  /// <param name="choreId">The unique identifier of the chore to update.</param>
  /// <param name="choreDto">An object containing the updated details for the chore. Cannot be null.</param>
  /// <param name="ct" </param>
  /// <returns>A tuple containing a status string indicating the result of the update operation, and a <see cref="ChoreDto"/>
  /// representing the updated chore. The chore value will be null if the update fails or the chore does not exist.</returns>
  Task<(string status, ChoreDto? chore)> UpdateChoreAsync(ChoreDto choreDto, CancellationToken ct);
}