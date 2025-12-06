using System.Text.Json;
using FamMan.Api.Events.Dtos;
using FamMan.Api.Events.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Events;

public static class ActionableEventsEndpoints
{
  public static void MapActionableEventsEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/events")
        .WithTags("Actionable Events");

    group
      .MapPost("/", CreateEvent)
      .WithName("CreateEvent")
      .WithSummary("Creates a new event")
      .WithDescription("Creates a new event, otherwise 500")
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status500InternalServerError);

    group
      .MapPut("/{id}", UpdateEvent)
      .WithName("UpdateEvent")
      .WithSummary("Updates an event")
      .WithDescription("Updates an event")
      .Produces(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound);

    group
      .MapGet("/", GetAllEvents)
      .WithName("GetAllEvents")
      .WithSummary("Retrieves all events")
      .WithDescription("Returns all events, or an empty list if no events")
      .Produces<List<ActionableEventDto>>(StatusCodes.Status200OK);

    group
      .MapGet("/{id}", GetEventById)
      .WithName("GetEventById")
      .WithSummary("Retrieves a specific event")
      .WithDescription("Returns the event details if found, otherwise 404")
      .Produces<ActionableEventDto>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound);

    group
      .MapDelete("/{id}", DeleteEvent)
      .WithName("DeleteEvent")
      .WithSummary("Deletes an event")
      .WithDescription("Deletes an event, otherwise 500")
      .Produces(StatusCodes.Status204NoContent)
      .Produces(StatusCodes.Status500InternalServerError);
  }
  private async static Task<Results<Created<ActionableEventDto>, BadRequest<ValidationProblemDetails>>> CreateEvent(
    [FromBody] ActionableEventDto actionableEvent,
    [FromServices] IEventService eventService,
    CancellationToken ct
  )
  {
    var createdEvent = await eventService.CreateEventAsync(actionableEvent, ct);
    return TypedResults.Created($"/api/events/{createdEvent.Id}", createdEvent);
  }
  private async static Task<Results<Ok<ActionableEventDto>, NotFound, BadRequest>> UpdateEvent(
    [FromRoute] Guid id,
    [FromBody] ActionableEventDto actionableEvent,
    [FromServices] IEventService eventService,
    CancellationToken ct
  )
  {
    if (id != actionableEvent.Id)
    {
      return TypedResults.BadRequest();
    }

    var (status, updatedEvent) = await eventService.UpdateEventAsync(actionableEvent, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedEvent);
  }
  private async static Task<Ok<List<ActionableEventDto>>> GetAllEvents(
    [FromServices] IEventService eventService,
    CancellationToken ct
  )
  {
    var result = await eventService.GetAllEventsAsync(ct);
    return TypedResults.Ok(result);
  }
  private async static Task<Results<Ok<ActionableEventDto>, NotFound>> GetEventById(
    [FromRoute] Guid id,
    [FromServices] IEventService eventService,
    CancellationToken ct
  )
  {
    var (status, actionableEvent) = await eventService.GetEventByIdAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(actionableEvent);
  }
  private async static Task<NoContent> DeleteEvent(
    [FromRoute] Guid id,
    [FromServices] IEventService eventService,
    CancellationToken ct
  )
  {
    await eventService.DeleteEventAsync(id, ct);
    return TypedResults.NoContent();
  }
}