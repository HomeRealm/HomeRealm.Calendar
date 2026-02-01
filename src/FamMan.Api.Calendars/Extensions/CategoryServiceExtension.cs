using FamMan.Api.Calendars.Dtos.Categories;
using FamMan.Api.Calendars.Interfaces.Categories;
using FamMan.Api.Calendars.Services.Categories;
using FamMan.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class CategoryServiceExtension
{
  public static IServiceCollection AddCategoryServices(this IServiceCollection services)
  {
    services.TryAddTransient<ICategoryService, CategoryService>();
    services.TryAddScoped<ICategoryDataStore, CategoryDataStore>();
    services.TryAddTransient<IValidator<CategoryDto>, CategoryDtoValidator>();
    return services;
  }
}
