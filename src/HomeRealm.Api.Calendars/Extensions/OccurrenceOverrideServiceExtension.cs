using HomeRealm.Api.Calendars.Dtos.OccurrenceOverrides;
using HomeRealm.Api.Calendars.Interfaces.OccurrenceOverrides;
using HomeRealm.Api.Calendars.Services.OccurrenceOverrides;
using HomeRealm.Api.Calendars.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeRealm.Api.Calendars.Extensions;

public static class OccurrenceOverrideServiceExtension
{
  public static IServiceCollection AddOccurrenceOverrideServices(this IServiceCollection services)
  {
    services.TryAddTransient<IOccurrenceOverrideService, OccurrenceOverrideService>();
    services.TryAddScoped<IOccurrenceOverrideDataStore, OccurrenceOverrideDataStore>();
    services.TryAddTransient<IValidator<OccurrenceOverrideDto>, OccurrenceOverrideDtoValidator>();
    return services;
  }
}

