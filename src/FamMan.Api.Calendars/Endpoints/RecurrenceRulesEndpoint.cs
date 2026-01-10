using FamMan.Api.Calendars.Dtos.RecurrenceRules;
using FamMan.Api.Calendars.Interfaces.RecurrenceRules;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Calendars.Endpoints;

public static class RecurrenceRulesEndpoints
{
  public static void MapRecurrenceRulesEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/recurrencerules")
        .WithTags("RecurrenceRules");

    group
      .MapPost("/", CreateRecurrenceRule)
      .WithName("CreateRecurrenceRule")
      .WithSummary("Creates a new recurrence rule")
      .WithDescription("Creates a new recurrence rule");

    group
      .MapGet("/", GetAllRecurrenceRules)
      .WithName("GetAllRecurrenceRules")
      .WithSummary("Gets all recurrence rules")
      .WithDescription("Gets all recurrence rules");

    group
      .MapPut("/{id}", UpdateRecurrenceRule)
      .WithName("UpdateRecurrenceRule")
      .WithSummary("Updates a recurrence rule")
      .WithDescription("Updates the recurrence rule with the matching ID");

    group
      .MapGet("/{id}", GetRecurrenceRule)
      .WithName("GetRecurrenceRule")
      .WithSummary("Gets a recurrence rule")
      .WithDescription("Gets the recurrence rule with the matching ID");

    group
      .MapDelete("/{id}", DeleteRecurrenceRule)
      .WithName("DeleteRecurrenceRule")
      .WithSummary("Deletes a recurrence rule")
      .WithDescription("Deletes the recurrence rule with the matching ID");
  }
  private async static Task<Results<Created<RecurrenceRuleResponseDto>, ValidationProblem>> CreateRecurrenceRule(
    [FromBody] RecurrenceRuleDto dto,
    [FromServices] IRecurrenceRuleService recurrenceRuleService,
    [FromServices] IValidator<RecurrenceRuleDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdRecurrenceRule = await recurrenceRuleService.CreateRecurrenceRuleAsync(dto, ct);
    return TypedResults.Created($"/api/recurrencerules/{createdRecurrenceRule.Id}", createdRecurrenceRule);
  }
  private async static Task<Results<Ok<RecurrenceRuleResponseDto>, NotFound, ValidationProblem>> UpdateRecurrenceRule(
    [FromRoute] Guid id,
    [FromBody] RecurrenceRuleDto dto,
    [FromServices] IRecurrenceRuleService recurrenceRuleService,
    [FromServices] IValidator<RecurrenceRuleDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedRecurrenceRule) = await recurrenceRuleService.UpdateRecurrenceRuleAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedRecurrenceRule);
  }
  private async static Task<Results<Ok<RecurrenceRuleResponseDto>, NotFound>> GetRecurrenceRule(
    [FromRoute] Guid id,
    [FromServices] IRecurrenceRuleService recurrenceRuleService,
    CancellationToken ct
  )
  {
    var (status, recurrenceRule) = await recurrenceRuleService.GetRecurrenceRuleAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(recurrenceRule);
  }
  private async static Task<Ok<List<RecurrenceRuleResponseDto>>> GetAllRecurrenceRules(
    [FromServices] IRecurrenceRuleService recurrenceRuleService,
    CancellationToken ct
  )
  {
    var recurrenceRules = await recurrenceRuleService.GetAllRecurrenceRulesAsync(ct);
    return TypedResults.Ok(recurrenceRules);
  }
  private async static Task<NoContent> DeleteRecurrenceRule(
    [FromRoute] Guid id,
    [FromServices] IRecurrenceRuleService recurrenceRuleService,
    CancellationToken ct
  )
  {
    await recurrenceRuleService.DeleteRecurrenceRuleAsync(id, ct);
    return TypedResults.NoContent();
  }
}

