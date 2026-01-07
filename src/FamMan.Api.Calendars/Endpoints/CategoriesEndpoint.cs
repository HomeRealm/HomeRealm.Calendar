using FamMan.Api.Calendars.Dtos.Category;
using FamMan.Api.Calendars.Interfaces.Category;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamMan.Api.Calendars.Endpoints;

public static class CategoriesEndpoints
{
  public static void MapCategoriesEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var group = endpoints.MapGroup("/categories")
        .WithTags("Categories");

    group
      .MapPost("/", CreateCategory)
      .WithName("CreateCategory")
      .WithSummary("Creates a new category")
      .WithDescription("Creates a new category");

    group
      .MapGet("/", GetAllCategories)
      .WithName("GetAllCategories")
      .WithSummary("Gets all categories")
      .WithDescription("Gets all categories");

    group
      .MapPut("/{id}", UpdateCategory)
      .WithName("UpdateCategory")
      .WithSummary("Updates a category")
      .WithDescription("Updates the category with the matching ID");

    group
      .MapGet("/{id}", GetCategory)
      .WithName("GetCategory")
      .WithSummary("Gets a category")
      .WithDescription("Gets the category with the matching ID");

    group
      .MapDelete("/{id}", DeleteCategory)
      .WithName("DeleteCategory")
      .WithSummary("Deletes a category")
      .WithDescription("Deletes the category with the matching ID");
  }
  private async static Task<Results<Created<CategoryResponseDto>, ValidationProblem>> CreateCategory(
    [FromBody] CategoryRequestDto dto,
    [FromServices] ICategoryService categoryService,
    [FromServices] IValidator<CategoryRequestDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var createdCategory = await categoryService.CreateCategoryAsync(dto, ct);
    return TypedResults.Created($"/api/categories/{createdCategory.Id}", createdCategory);
  }
  private async static Task<Results<Ok<CategoryResponseDto>, NotFound, ValidationProblem>> UpdateCategory(
    [FromRoute] Guid id,
    [FromBody] CategoryRequestDto dto,
    [FromServices] ICategoryService categoryService,
    [FromServices] IValidator<CategoryRequestDto> validator,
    CancellationToken ct
  )
  {
    var validationResult = await validator.ValidateAsync(dto, ct);

    if (!validationResult.IsValid)
    {
      return TypedResults.ValidationProblem(validationResult.ToDictionary());
    }

    var (status, updatedCategory) = await categoryService.UpdateCategoryAsync(dto, id, ct);
    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(updatedCategory);
  }
  private async static Task<Results<Ok<CategoryResponseDto>, NotFound>> GetCategory(
    [FromRoute] Guid id,
    [FromServices] ICategoryService categoryService,
    CancellationToken ct
  )
  {
    var (status, category) = await categoryService.GetCategoryAsync(id, ct);

    return status == "notfound" ? TypedResults.NotFound() : TypedResults.Ok(category);
  }
  private async static Task<Ok<List<CategoryResponseDto>>> GetAllCategories(
    [FromServices] ICategoryService categoryService,
    CancellationToken ct
  )
  {
    var categories = await categoryService.GetAllCategoriesAsync(ct);
    return TypedResults.Ok(categories);
  }
  private async static Task<NoContent> DeleteCategory(
    [FromRoute] Guid id,
    [FromServices] ICategoryService categoryService,
    CancellationToken ct
  )
  {
    await categoryService.DeleteCategoryAsync(id, ct);
    return TypedResults.NoContent();
  }
}
