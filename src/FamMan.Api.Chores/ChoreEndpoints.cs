using FamMan.Api.Chores.Dtos;
using FamMan.Api.Chores.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Chores;

public static class ChoreEndpoints
{
  public static RouteGroupBuilder MapChoreEndpoints(this RouteGroupBuilder group)
  {
    var chores = group.MapGroup("/chores")
      .WithTags("Chores");

    chores.MapGet("/", GetChores)
      .WithName("GetChores")
      .WithSummary("Get all chores")
      .WithDescription("Retrieves a list of all chores in the system");

    chores.MapGet("/{id}", GetChoreById)
      .WithName("GetChoreById")
      .WithSummary("Get a chore by ID")
      .WithDescription("Retrieves a specific chore by its unique identifier");

    chores.MapPost("/", CreateChore)
      .WithName("CreateChore")
      .WithSummary("Create a new chore")
      .WithDescription("Creates a new chore with the provided details");

    chores.MapPut("/{id}", UpdateChore)
      .WithName("UpdateChore")
      .WithSummary("Update an existing chore")
      .WithDescription("Updates an existing chore with the provided details");

    chores.MapDelete("/{id}", DeleteChore)
      .WithName("DeleteChore")
      .WithSummary("Delete a chore")
      .WithDescription("Deletes a chore by its unique identifier");

    return group;
  }
  private static async Task<Ok<List<ChoreDto>>> GetChores(
    [FromServices] IChoreService choreService,
    CancellationToken ct)
  {
    var chores = await choreService.GetChoresAsync(ct);
    return TypedResults.Ok(chores);
  }
  private static async Task<Results<Ok<ChoreDto>, NotFound>> GetChoreById(
    [FromServices] IChoreService choreService,
    [FromRoute] Guid id,
    CancellationToken ct)
  {
    var (status, chore) = await choreService.GetChoreAsync(id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(chore);
  }
  private static async Task<Results<Created<ChoreDto>, BadRequest<ValidationProblemDetails>>> CreateChore(
    [FromServices] IChoreService choreService,
    [FromBody] ChoreDto chore,
    CancellationToken ct)
  {
    var savedChore = await choreService.CreateChoreAsync(chore, ct);
    return TypedResults.Created($"/api/chores/{savedChore.Id}", savedChore);
  }
  private static async Task<Results<Ok<ChoreDto>, NotFound, BadRequest>> UpdateChore(
    [FromServices] IChoreService choreService,
    [FromRoute] Guid id,
    [FromBody] ChoreDto chore,
    CancellationToken ct)
  {
    if (id != chore.Id)
    {
      return TypedResults.BadRequest();
    }
    var (status, modifiedChore) = await choreService.UpdateChoreAsync(chore, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(modifiedChore);
  }
  private static async Task<NoContent> DeleteChore(
    [FromServices] IChoreService choreService,
    [FromBody] Guid id,
    CancellationToken ct)
  {
    await choreService.DeleteChoreAsync(id, ct);
    return TypedResults.NoContent();
  }
}
