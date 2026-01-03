using FamMan.Api.Calendars.Dtos;
using FamMan.Api.Calendars.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Calendars.Endpoints;

public static class CalendarsEndpoints
{
  public static void MapCalendarsEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/calendars")
        .WithTags("Calendars");

    group
      .MapPost("/", CreateCalendar)
      .WithName("CreateCalendar")
      .WithSummary("Creates a new calendar")
      .WithDescription("Creates a new calendar")
      .Produces<CalendarResponseDto>(StatusCodes.Status200OK)
      .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest);
    group
      .MapPut("/{id}", UpdateCalendar)
      .WithName("UpdateCalendar")
      .WithSummary("Updates a calendar")
      .WithDescription("Updates the calendar with the matching ID")
      .Produces<CalendarResponseDto>(StatusCodes.Status200OK)
      .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status404NotFound);
    group
      .MapGet("/{id}", GetCalendar)
      .WithName("GetCalendar")
      .WithSummary("Gets a calendar")
      .WithDescription("Gets the calendar with the matching ID")
      .Produces<CalendarResponseDto>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound);
  }
  private async static Task<Results<Created<CalendarResponseDto>, ValidationProblem>> CreateCalendar(
    [FromBody] CalendarRequestDto dto,
    [FromServices] ICalendarService calendarService,
    [FromServices] IValidator<CalendarRequestDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdCalendar = await calendarService.CreateCalendarAsync(dto, ct);
    return TypedResults.Created($"/api/calendars/{createdCalendar.Id}", createdCalendar);
  }
  private async static Task<Results<Ok<CalendarResponseDto>, NotFound, ValidationProblem>> UpdateCalendar(
    [FromRoute] Guid id,
    [FromBody] CalendarRequestDto dto,
    [FromServices] ICalendarService calendarService,
    [FromServices] IValidator<CalendarRequestDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedCalendar) = await calendarService.UpdateCalendarAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedCalendar);
  }
  private async static Task<Results<Ok<CalendarResponseDto>, NotFound>> GetCalendar(
    [FromRoute] Guid id,
    [FromServices] ICalendarService calendarService,
    CancellationToken ct
  )
  {
    var (status, calendar) = await calendarService.GetCalendarAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(calendar);
  }
  // private async static Task<NoContent> DeleteEvent(
  //   [FromRoute] Guid id,
  //   [FromServices] IEventService eventService,
  //   CancellationToken ct
  // )
  // {
  //   await eventService.DeleteEventAsync(id, ct);
  //   return TypedResults.NoContent();
  // }
}