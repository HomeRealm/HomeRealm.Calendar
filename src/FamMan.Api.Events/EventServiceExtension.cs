using FamMan.Api.Events.Interfaces;
using FamMan.Api.Events.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Events;

public static class EventServiceExtension
{
  public static IServiceCollection AddEventServices(this IServiceCollection services)
  {
    // Register chore-related services here
    // e.g., services.AddScoped<IChoreService, ChoreService>();
    services.TryAddTransient<IEventService, EventService>();
    services.TryAddScoped<IEventDataStore, EventDataStore>();
    return services;
  }
}
