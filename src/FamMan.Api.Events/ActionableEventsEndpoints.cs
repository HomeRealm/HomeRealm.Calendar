using FamMan.Api.Events.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamMan.Api.Events
{
    public static class ActionableEventsEndpoints
    {
        public static void MapActionableEventsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/events")
                .WithTags("Actionable Events");
            group.MapGet("/", () =>
            {
                return Results.Ok(new ActionableEventDto
                {
                    Guid = Guid.CreateVersion7()
                });
            });
        }
    }
}
