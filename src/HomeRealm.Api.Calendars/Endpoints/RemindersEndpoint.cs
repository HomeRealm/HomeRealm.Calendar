using HomeRealm.Api.Calendars.Interfaces.Reminders;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using HomeRealm.Api.Calendars.Dtos.Reminders;

namespace HomeRealm.Api.Calendars.Endpoints;

public static class RemindersEndpoints
{
  public static void MapRemindersEndpoints(this IEndpointRouteBuilder endpoints, RouteGroupBuilder eventsRoute)
  {
    // /api/events/{eventId}/reminders
    eventsRoute
      .MapGet("/reminders", GetRemindersForCalendarEvent)
      .WithName("GetRemindersForCalendarEvent")
      .WithSummary("Gets all reminders for a specific event")
      .WithDescription("Gets all reminders for a specific event");

    // /api/reminders
    var group = endpoints.MapGroup("/reminders")
      .WithTags("Reminders");

    group
      .MapPost("/", CreateReminder)
      .WithName("CreateReminder")
      .WithSummary("Creates a new reminder")
      .WithDescription("Creates a new reminder");

    group
      .MapGet("/", GetAllReminders)
      .WithName("GetAllReminders")
      .WithSummary("Gets all reminders")
      .WithDescription("Gets all reminders");

    group
      .MapPut("/{id}", UpdateReminder)
      .WithName("UpdateReminder")
      .WithSummary("Updates a reminder")
      .WithDescription("Updates the reminder with the matching ID");

    group
      .MapGet("/{id}", GetReminder)
      .WithName("GetReminder")
      .WithSummary("Gets a reminder")
      .WithDescription("Gets the reminder with the matching ID");

    group
      .MapDelete("/{id}", DeleteReminder)
      .WithName("DeleteReminder")
      .WithSummary("Deletes a reminder")
      .WithDescription("Deletes the reminder with the matching ID");
  }
  private async static Task<Results<Created<ReminderResponseDto>, ValidationProblem>> CreateReminder(
    [FromBody] ReminderDto dto,
    [FromServices] IReminderService reminderService,
    [FromServices] IValidator<ReminderDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdReminder = await reminderService.CreateReminderAsync(dto, ct);
    return TypedResults.Created($"/api/reminders/{createdReminder.Id}", createdReminder);
  }
  private async static Task<Results<Ok<ReminderResponseDto>, NotFound, ValidationProblem>> UpdateReminder(
    [FromRoute] Guid id,
    [FromBody] ReminderDto dto,
    [FromServices] IReminderService reminderService,
    [FromServices] IValidator<ReminderDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedReminder) = await reminderService.UpdateReminderAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedReminder);
  }
  private async static Task<Results<Ok<ReminderResponseDto>, NotFound>> GetReminder(
    [FromRoute] Guid id,
    [FromServices] IReminderService reminderService,
    CancellationToken ct
  )
  {
    var (status, reminder) = await reminderService.GetReminderAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(reminder);
  }
  private async static Task<Ok<List<ReminderResponseDto>>> GetRemindersForCalendarEvent(
    [FromRoute] Guid eventId,
    [FromServices] IReminderService reminderService,
    CancellationToken ct
  )
  {
    var reminders = await reminderService.GetAllRemindersAsync(ct);
    return TypedResults.Ok(reminders);
  }
  private async static Task<Ok<List<ReminderResponseDto>>> GetAllReminders(
    [FromServices] IReminderService reminderService,
    CancellationToken ct
  )
  {
    var reminders = await reminderService.GetAllRemindersAsync(ct);
    return TypedResults.Ok(reminders);
  }
  private async static Task<NoContent> DeleteReminder(
    [FromRoute] Guid id,
    [FromServices] IReminderService reminderService,
    CancellationToken ct
  )
  {
    await reminderService.DeleteReminderAsync(id, ct);
    return TypedResults.NoContent();
  }
}

