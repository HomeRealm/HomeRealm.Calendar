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
