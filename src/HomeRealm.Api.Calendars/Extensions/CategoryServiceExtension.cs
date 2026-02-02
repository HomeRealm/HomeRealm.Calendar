using HomeRealm.Api.Calendars.Dtos.Categories;
using HomeRealm.Api.Calendars.Interfaces.Categories;
using HomeRealm.Api.Calendars.Services.Categories;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

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
