using HomeRealm.Api.Calendars.Dtos.CalendarEvents;
using HomeRealm.Api.Calendars.Interfaces.CalendarEvents;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HomeRealm.Api.Calendars.Endpoints;

public static class CalendarEventsEndpoints
{
  public static RouteGroupBuilder MapCalendarEventsEndpoints(this IEndpointRouteBuilder endpoints, IEndpointRouteBuilder calendarsRoute)
  {
    // /api/calendars/{calendarId}/events
    calendarsRoute
      .MapGet("/events", GetCalendarEventsForCalendar)
      .WithName("GetCalendarEventsForCalendar")
      .WithSummary("Gets all events for a specific calendar")
      .WithDescription("Gets all events for a specific calendar");

    // /api/calendars/{calendarId}/occurrences
    calendarsRoute
      .MapGet("/occurrences", GetCalendarOccurrences)
      .WithName("GetCalendarOccurrences")
      .WithSummary("Gets all occurrences for a specific calendar")
      .WithDescription("Gets all occurrences for a specific calendar");

    // /api/events
    var group = endpoints.MapGroup("/events")
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

    // /api/events/{id}/occurrences
    group
      .MapGet("/{id}/occurrences", GetEventOccurrences)
      .WithName("GetEventOccurrences")
      .WithSummary("Gets all occurrences for a specific event")
      .WithDescription("Gets all occurrences for a specific event");

    return group;
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
  private async static Task<Ok<List<CalendarEventResponseDto>>> GetCalendarEventsForCalendar(
  [FromRoute] Guid calendarId,
  [FromServices] ICalendarEventService calendarEventService,
  CancellationToken ct
)
  {
    var calendarEvents = await calendarEventService.GetCalendarEventsForCalendarAsync(calendarId, ct);
    return TypedResults.Ok(calendarEvents);
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
  private async static Task<Ok<List<DateTime>>> GetCalendarOccurrences(
      [FromQuery] DateTime start,
      [FromQuery] DateTime end,
      CancellationToken ct
    )
  {
    return TypedResults.Ok(new List<DateTime>() { start, end });
  }
  private async static Task<Ok<List<DateTime>>> GetEventOccurrences(
      [FromQuery] DateTime start,
      [FromQuery] DateTime end,
      CancellationToken ct
    )
  {
    return TypedResults.Ok(new List<DateTime>() { start, end });
  }
}


