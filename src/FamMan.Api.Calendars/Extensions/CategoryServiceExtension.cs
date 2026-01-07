using FamMan.Api.Calendars.Interfaces.Category;
using FamMan.Api.Calendars.Services.Category;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Calendars.Extensions;

public static class CategoryServiceExtension
{
  public static IServiceCollection AddCategoryServices(this IServiceCollection services)
  {
    services.TryAddTransient<ICategoryService, CategoryService>();
    services.TryAddScoped<ICategoryDataStore, CategoryDataStore>();
    return services;
  }
}
