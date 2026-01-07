using FamMan.Api.Calendars.Dtos.Attendee;
using FamMan.Api.Calendars.Interfaces.Attendee;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Calendars.Endpoints;

public static class AttendeesEndpoints
{
  public static void MapAttendeesEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/attendees")
        .WithTags("Attendees");

    group
      .MapPost("/", CreateAttendee)
      .WithName("CreateAttendee")
      .WithSummary("Creates a new attendee")
      .WithDescription("Creates a new attendee");

    group
      .MapGet("/", GetAllAttendees)
      .WithName("GetAllAttendees")
      .WithSummary("Gets all attendees")
      .WithDescription("Gets all attendees");

    group
      .MapPut("/{id}", UpdateAttendee)
      .WithName("UpdateAttendee")
      .WithSummary("Updates an attendee")
      .WithDescription("Updates the attendee with the matching ID");

    group
      .MapGet("/{id}", GetAttendee)
      .WithName("GetAttendee")
      .WithSummary("Gets an attendee")
      .WithDescription("Gets the attendee with the matching ID");

    group
      .MapDelete("/{id}", DeleteAttendee)
      .WithName("DeleteAttendee")
      .WithSummary("Deletes an attendee")
      .WithDescription("Deletes the attendee with the matching ID");
  }
  private async static Task<Results<Created<AttendeeResponseDto>, ValidationProblem>> CreateAttendee(
    [FromBody] AttendeeRequestDto dto,
    [FromServices] IAttendeeService attendeeService,
    [FromServices] IValidator<AttendeeRequestDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdAttendee = await attendeeService.CreateAttendeeAsync(dto, ct);
    return TypedResults.Created($"/api/attendees/{createdAttendee.Id}", createdAttendee);
  }
  private async static Task<Results<Ok<AttendeeResponseDto>, NotFound, ValidationProblem>> UpdateAttendee(
    [FromRoute] Guid id,
    [FromBody] AttendeeRequestDto dto,
    [FromServices] IAttendeeService attendeeService,
    [FromServices] IValidator<AttendeeRequestDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedAttendee) = await attendeeService.UpdateAttendeeAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedAttendee);
  }
  private async static Task<Results<Ok<AttendeeResponseDto>, NotFound>> GetAttendee(
    [FromRoute] Guid id,
    [FromServices] IAttendeeService attendeeService,
    CancellationToken ct
  )
  {
    var (status, attendee) = await attendeeService.GetAttendeeAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(attendee);
  }
  private async static Task<Ok<List<AttendeeResponseDto>>> GetAllAttendees(
    [FromServices] IAttendeeService attendeeService,
    CancellationToken ct
  )
  {
    var attendees = await attendeeService.GetAllAttendeesAsync(ct);
    return TypedResults.Ok(attendees);
  }
  private async static Task<NoContent> DeleteAttendee(
    [FromRoute] Guid id,
    [FromServices] IAttendeeService attendeeService,
    CancellationToken ct
  )
  {
    await attendeeService.DeleteAttendeeAsync(id, ct);
    return TypedResults.NoContent();
  }
}
