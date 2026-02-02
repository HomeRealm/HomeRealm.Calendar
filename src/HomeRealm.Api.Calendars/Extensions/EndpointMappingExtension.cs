using HomeRealm.Api.Calendars.Endpoints;

namespace HomeRealm.Api.Calendars.Extensions;

public static class EndpointMappingExtension
{
  public static void MapAllEndpoints(this IEndpointRouteBuilder endpoints)
  {
    var calendarBaseEndpointGroup = endpoints.MapCalendarsEndpoints();
    var calendarResourceGroup = calendarBaseEndpointGroup.MapGroup("/{calendarId}");

    var eventsBaseEndpointGroup = endpoints.MapCalendarEventsEndpoints(calendarResourceGroup);
    var eventsResourceGroup = eventsBaseEndpointGroup.MapGroup("/{eventId}");

    var recurrenceBaseEndpointGroup = endpoints.MapRecurrenceRulesEndpoints(eventsResourceGroup);
    var recurrenceResourceGroup = recurrenceBaseEndpointGroup.MapGroup("/{recurrenceId}");

    endpoints.MapOccurrenceOverridesEndpoints(recurrenceResourceGroup);
    endpoints.MapAttendeesEndpoints(eventsResourceGroup);
    endpoints.MapRemindersEndpoints(eventsResourceGroup);
    endpoints.MapCategoriesEndpoints();
  }
}
