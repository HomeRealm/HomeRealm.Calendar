using System.Runtime.CompilerServices;
using FamMan.Api.Chores.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FamMan.Api.Events;

public static class ChoreServiceExtensions
{
    public static IServiceCollection AddChoreServices(this IServiceCollection services)
    {
        // Register chore-related services here
        // e.g., services.AddScoped<IChoreService, ChoreService>();
        services.TryAddTransient<IChoreService, ChoreService>();
        services.TryAddScoped<IChoreDataStore, ChoreDataStore>();
        return services;
    }
}
