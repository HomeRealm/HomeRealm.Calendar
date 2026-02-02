
# Resource to ICAL Conversion

This guide explains how Calendar API resources map to ICAL fields using ical.net, and how occurrences are returned as JSON in the API.

---

## Mapping Table

| Resource         | ICAL Field/Class      | Notes                                   |
|------------------|----------------------|-----------------------------------------|
| Calendar         | VCALENDAR            | Top-level calendar container            |
| CalendarEvent    | VEVENT               | Main event object                       |
| RecurrenceRule   | RRULE                | Recurrence pattern (RFC 5545)           |
| OccurrenceOverride | EXDATE/RECURRENCE-ID | Skipped/modified occurrences            |
| Attendee         | ATTENDEE             | Event participants                      |
| Reminder         | VALARM               | Event notifications                     |
| Category         | CATEGORIES           | Event categorization                    |

---


## Conversion Steps

1. **Create VCALENDAR** for each Calendar.
2. **Add VEVENT** for each CalendarEvent.
3. **Set RRULE** on VEVENT if event is recurring.
4. **Add EXDATE/RECURRENCE-ID** for occurrence overrides.
5. **Add ATTENDEE** fields for participants.
6. **Add VALARM** for reminders.
7. **Set CATEGORIES** for event tags.

---

## Occurrence Generation as JSON

While ical.net is used to generate event occurrences according to ICAL standards, the homerealm Calendar API returns these occurrences as JSON objects. This makes it easier for client applications to consume and display event data.

### Example JSON Occurrence Output

```json
{
    "id": "evt-123",
    "calendarId": "cal-1",
    "title": "Doctor Appointment",
    "start": "2026-01-10T09:00:00Z",
    "end": "2026-01-10T10:00:00Z",
    "location": "Clinic",
    "allDay": false,
    "recurrenceId": "rr-1",
    "category": "Appointment",
    "occurrenceDate": "2026-01-10T09:00:00Z",
    "attendees": [
        { "userId": "user-1", "status": "accepted" }
    ],
    "reminders": [
        { "method": "push", "timeBefore": 30 }
    ]
}
```

### Notes

- The API leverages ical.net for recurrence logic, but serializes each occurrence as a JSON object.
- This approach provides the power of ICAL recurrence with the convenience of JSON for API consumers.

---

## Example (ical.net)

```csharp
var calendar = new Calendar();
var evt = new CalendarEvent
{
    Summary = "Doctor Appointment",
    DtStart = new CalDateTime(2026, 1, 10, 9, 0, 0),
    DtEnd = new CalDateTime(2026, 1, 10, 10, 0, 0),
    Location = "Clinic",
    Categories = new List<string> { "Appointment" }
};
calendar.Events.Add(evt);
```

---

## Notes

- Only occurrence overrides are stored; normal occurrences are generated from RRULE.
- Use custom X- fields for extra metadata if needed.
