using FamMan.Api.Calendars.Endpoints;

namespace FamMan.Api.Calendars.Extensions;

public static class EndpointMappingExtension
{
  public static RouteGroupBuilder MapAllEndpoints(this IEndpointRouteBuilder endpoints)
  {
    // configure calendar resource endpoints and store group.
    var calendarBaseEndpointGroup = endpoints.MapCalendarsEndpoints();

    return eventsBaseEndpointGroup;
  }
}