using FamMan.Api.Calendars.Dtos.Calendar;
using FamMan.Api.Calendars.Interfaces.Calendar;
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
      .WithDescription("Creates a new calendar");

    group
      .MapGet("/", GetAllCalendars)
      .WithName("GetAllCalendars")
      .WithSummary("Gets all calendars")
      .WithDescription("Gets all calendars");

    group
      .MapPut("/{id}", UpdateCalendar)
      .WithName("UpdateCalendar")
      .WithSummary("Updates a calendar")
      .WithDescription("Updates the calendar with the matching ID");

    group
      .MapGet("/{id}", GetCalendar)
      .WithName("GetCalendar")
      .WithSummary("Gets a calendar")
      .WithDescription("Gets the calendar with the matching ID");

    group
      .MapDelete("/{id}", DeleteCalendar)
      .WithName("DeleteCalendar")
      .WithSummary("Deletes a calendar")
      .WithDescription("Deletes the calendar with the matching ID");
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
  private async static Task<Ok<List<CalendarResponseDto>>> GetAllCalendars(
    [FromServices] ICalendarService calendarService,
    CancellationToken ct
  )
  {
    var calendars = await calendarService.GetAllCalendarsAsync(ct);
    return TypedResults.Ok(calendars);
  }
  private async static Task<NoContent> DeleteCalendar(
    [FromRoute] Guid id,
    [FromServices] ICalendarService calendarService,
    CancellationToken ct
  )
  {
    await calendarService.DeleteCalendarAsync(id, ct);
    return TypedResults.NoContent();
  }
}