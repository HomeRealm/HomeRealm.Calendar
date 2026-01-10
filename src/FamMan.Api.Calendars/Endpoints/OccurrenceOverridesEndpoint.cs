using FamMan.Api.Calendars.Dtos.OccurrenceOverrides;
using FamMan.Api.Calendars.Interfaces.OccurrenceOverrides;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Calendars.Endpoints;

public static class OccurrenceOverridesEndpoints
{
  public static void MapOccurrenceOverridesEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/occurrenceoverrides")
        .WithTags("OccurrenceOverrides");

    group
      .MapPost("/", CreateOccurrenceOverride)
      .WithName("CreateOccurrenceOverride")
      .WithSummary("Creates a new occurrence override")
      .WithDescription("Creates a new occurrence override");

    group
      .MapGet("/", GetAllOccurrenceOverrides)
      .WithName("GetAllOccurrenceOverrides")
      .WithSummary("Gets all occurrence overrides")
      .WithDescription("Gets all occurrence overrides");

    group
      .MapPut("/{id}", UpdateOccurrenceOverride)
      .WithName("UpdateOccurrenceOverride")
      .WithSummary("Updates an occurrence override")
      .WithDescription("Updates the occurrence override with the matching ID");

    group
      .MapGet("/{id}", GetOccurrenceOverride)
      .WithName("GetOccurrenceOverride")
      .WithSummary("Gets an occurrence override")
      .WithDescription("Gets the occurrence override with the matching ID");

    group
      .MapDelete("/{id}", DeleteOccurrenceOverride)
      .WithName("DeleteOccurrenceOverride")
      .WithSummary("Deletes an occurrence override")
      .WithDescription("Deletes the occurrence override with the matching ID");
  }
  private async static Task<Results<Created<OccurrenceOverrideResponseDto>, ValidationProblem>> CreateOccurrenceOverride(
    [FromBody] OccurrenceOverrideDto dto,
    [FromServices] IOccurrenceOverrideService occurrenceOverrideService,
    [FromServices] IValidator<OccurrenceOverrideDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdOccurrenceOverride = await occurrenceOverrideService.CreateOccurrenceOverrideAsync(dto, ct);
    return TypedResults.Created($"/api/occurrenceoverrides/{createdOccurrenceOverride.Id}", createdOccurrenceOverride);
  }
  private async static Task<Results<Ok<OccurrenceOverrideResponseDto>, NotFound, ValidationProblem>> UpdateOccurrenceOverride(
    [FromRoute] Guid id,
    [FromBody] OccurrenceOverrideDto dto,
    [FromServices] IOccurrenceOverrideService occurrenceOverrideService,
    [FromServices] IValidator<OccurrenceOverrideDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedOccurrenceOverride) = await occurrenceOverrideService.UpdateOccurrenceOverrideAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedOccurrenceOverride);
  }
  private async static Task<Results<Ok<OccurrenceOverrideResponseDto>, NotFound>> GetOccurrenceOverride(
    [FromRoute] Guid id,
    [FromServices] IOccurrenceOverrideService occurrenceOverrideService,
    CancellationToken ct
  )
  {
    var (status, occurrenceOverride) = await occurrenceOverrideService.GetOccurrenceOverrideAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(occurrenceOverride);
  }
  private async static Task<Ok<List<OccurrenceOverrideResponseDto>>> GetAllOccurrenceOverrides(
    [FromServices] IOccurrenceOverrideService occurrenceOverrideService,
    CancellationToken ct
  )
  {
    var occurrenceOverrides = await occurrenceOverrideService.GetAllOccurrenceOverridesAsync(ct);
    return TypedResults.Ok(occurrenceOverrides);
  }
  private async static Task<NoContent> DeleteOccurrenceOverride(
    [FromRoute] Guid id,
    [FromServices] IOccurrenceOverrideService occurrenceOverrideService,
    CancellationToken ct
  )
  {
    await occurrenceOverrideService.DeleteOccurrenceOverrideAsync(id, ct);
    return TypedResults.NoContent();
  }
}

