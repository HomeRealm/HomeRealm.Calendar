
# ICAL-Compatible Endpoint Guide

This guide explains how to build an endpoint that serves ICAL data for calendar integration.

---

## Endpoint Example

- `GET /calendars/{id}/ical`
  - Returns the calendar as an ICAL (.ics) file
  - Content-Type: `text/calendar`

## Implementation Steps

1. **Fetch Calendar and Events**
   - Retrieve calendar, events, recurrence rules, occurrence overrides, attendees, reminders, and categories.

2. **Map Resources to ical.net Objects**
   - Use ical.net to create VCALENDAR, VEVENT, RRULE, EXDATE, ATTENDEE, VALARM, CATEGORIES.

3. **Serialize to ICAL Format**
   - Use `calendar.Serialize()` from ical.net.

4. **Return as .ics File**
   - Set response headers for file download and correct MIME type.

---

## Example (C#)

```csharp
var calendar = BuildCalendarFromResources(...);
var icalString = calendar.Serialize();
return File(Encoding.UTF8.GetBytes(icalString), "text/calendar", "calendar.ics");
```

---

## Notes

- Ensure all recurring events and occurrence overrides are correctly mapped.
- Support filtering by date range if needed.
- Use authentication/authorization for private calendars.

---

This endpoint allows users to subscribe to FamMan calendars in external apps (Google Calendar, Outlook, Apple Calendar, etc.).
