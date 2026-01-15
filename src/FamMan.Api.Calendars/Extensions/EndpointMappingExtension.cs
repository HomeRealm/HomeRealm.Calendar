using FamMan.Api.Calendars.Endpoints;

namespace FamMan.Api.Calendars.Extensions;

public static class EndpointMappingExtension
{
  public static RouteGroupBuilder MapAllEndpoints(this IEndpointRouteBuilder endpoints)
  {
    // configure calendar resource endpoints and store group.
    var calendarBaseEndpointGroup = endpoints.MapCalendarsEndpoints();

    // create a sub group with the calendar id. 
    var calendarResourceGroup = calendarBaseEndpointGroup.MapGroup("/{calendarId}");

    var eventsBaseEndpointGroup = endpoints.MapCalendarEventsEndpoints(calendarResourceGroup);

    var eventsResourceGroup = eventsBaseEndpointGroup.MapGroup("/{eventId}");

    var recurrenceBaseEndpointGroup = endpoints.MapRecurrenceRulesEndpoints(eventsResourceGroup);

    var recurrenceResourceGroup = recurrenceBaseEndpointGroup.MapGroup("/{recurrenceId}");

    endpoints.MapOccurrenceOverridesEndpoints(recurrenceResourceGroup);
    endpoints.MapAttendeesEndpoints(eventsResourceGroup);

    return recurrenceBaseEndpointGroup;
  }
}