using FamMan.Api.Chores.Dtos;
using FamMan.Api.Chores.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamMan.Api.Chores.Services;

/// <inheritdoc/>
public class ChoreService(IChoreDataStore dataStore) : IChoreService
{
  /// <inheritdoc/>
  public async Task<List<ChoreDto>> GetChoresAsync(CancellationToken ct)
  {
    var chores = await dataStore.GetChores()
        .Select(chore => new ChoreDto
        {
          Id = chore.Id,
          Name = chore.Name,
          Description = chore.Description,
          CreatedAt = chore.CreatedAt,
          DueDate = chore.DueDate
        })
        .ToListAsync(ct);
    return chores;
  }

  /// <inheritdoc/>
  public async Task<(string status, ChoreDto? chore)> GetChoreAsync(Guid choreId, CancellationToken ct)
  {
    var chore = await dataStore.GetChoreAsync(choreId, ct);
    if (chore is null)
    {
      return ("notfound", null);
    }
    return ("found", MapToDto(chore));
  }

  /// <inheritdoc/>
  public async Task<ChoreDto> CreateChoreAsync(ChoreDto choreDto, CancellationToken ct)
  {
    var chore = MapToEntity(choreDto);

    var createdChore = await dataStore.CreateChoreAsync(chore, ct);

    return MapToDto(createdChore);
  }

  /// <inheritdoc/>
  public async Task<(string status, ChoreDto? chore)> UpdateChoreAsync(ChoreDto choreDto, CancellationToken ct)
  {
    var existingChore = await dataStore.GetChoreAsync(choreDto.Id, ct);
    if (existingChore is null)
    {
      return ("notfound", null);
    }
    var modifiedChore = MapToEntity(choreDto);

    var chore = await dataStore.UpdateChoreAsync(existingChore, modifiedChore, ct);
    return ("updated", MapToDto(chore));

  }

  /// <inheritdoc/>
  public Task DeleteChoreAsync(Guid choreId, CancellationToken ct)
  {
    return dataStore.DeleteChoreAsync(choreId, ct);
  }

  private Chore MapToEntity(ChoreDto choreDto) => new()
  {
    Id = choreDto.Id,
    Name = choreDto.Name,
    Description = choreDto.Description,
    CreatedAt = choreDto.CreatedAt,
    DueDate = choreDto.DueDate
  };
  private ChoreDto MapToDto(Chore chore) => new()
  {
    Id = chore.Id,
    Name = chore.Name,
    Description = chore.Description,
    CreatedAt = chore.CreatedAt,
    DueDate = chore.DueDate
  };
}
