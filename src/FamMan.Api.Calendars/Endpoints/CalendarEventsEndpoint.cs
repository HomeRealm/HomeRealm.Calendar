using FamMan.Api.Calendars.Dtos.CalendarEvents;
using FamMan.Api.Calendars.Interfaces.CalendarEvents;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Calendars.Endpoints;

public static class CalendarEventsEndpoints
{
  public static void MapCalendarEventsEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/calendarevents")
        .WithTags("CalendarEvents");

    group
      .MapPost("/", CreateCalendarEvent)
      .WithName("CreateCalendarEvent")
      .WithSummary("Creates a new calendar event")
      .WithDescription("Creates a new calendar event");

    group
      .MapGet("/", GetAllCalendarEvents)
      .WithName("GetAllCalendarEvents")
      .WithSummary("Gets all calendar events")
      .WithDescription("Gets all calendar events");

    group
      .MapPut("/{id}", UpdateCalendarEvent)
      .WithName("UpdateCalendarEvent")
      .WithSummary("Updates a calendar event")
      .WithDescription("Updates the calendar event with the matching ID");

    group
      .MapGet("/{id}", GetCalendarEvent)
      .WithName("GetCalendarEvent")
      .WithSummary("Gets a calendar event")
      .WithDescription("Gets the calendar event with the matching ID");

    group
      .MapDelete("/{id}", DeleteCalendarEvent)
      .WithName("DeleteCalendarEvent")
      .WithSummary("Deletes a calendar event")
      .WithDescription("Deletes the calendar event with the matching ID");
  }
  private async static Task<Results<Created<CalendarEventResponseDto>, ValidationProblem>> CreateCalendarEvent(
    [FromBody] CalendarEventDto dto,
    [FromServices] ICalendarEventService calendarEventService,
    [FromServices] IValidator<CalendarEventDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdCalendarEvent = await calendarEventService.CreateCalendarEventAsync(dto, ct);
    return TypedResults.Created($"/api/calendarevents/{createdCalendarEvent.Id}", createdCalendarEvent);
  }
  private async static Task<Results<Ok<CalendarEventResponseDto>, NotFound, ValidationProblem>> UpdateCalendarEvent(
    [FromRoute] Guid id,
    [FromBody] CalendarEventDto dto,
    [FromServices] ICalendarEventService calendarEventService,
    [FromServices] IValidator<CalendarEventDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedCalendarEvent) = await calendarEventService.UpdateCalendarEventAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedCalendarEvent);
  }
  private async static Task<Results<Ok<CalendarEventResponseDto>, NotFound>> GetCalendarEvent(
    [FromRoute] Guid id,
    [FromServices] ICalendarEventService calendarEventService,
    CancellationToken ct
  )
  {
    var (status, calendarEvent) = await calendarEventService.GetCalendarEventAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(calendarEvent);
  }
  private async static Task<Ok<List<CalendarEventResponseDto>>> GetAllCalendarEvents(
    [FromServices] ICalendarEventService calendarEventService,
    CancellationToken ct
  )
  {
    var calendarEvents = await calendarEventService.GetAllCalendarEventsAsync(ct);
    return TypedResults.Ok(calendarEvents);
  }
  private async static Task<NoContent> DeleteCalendarEvent(
    [FromRoute] Guid id,
    [FromServices] ICalendarEventService calendarEventService,
    CancellationToken ct
  )
  {
    await calendarEventService.DeleteCalendarEventAsync(id, ct);
    return TypedResults.NoContent();
  }
}

