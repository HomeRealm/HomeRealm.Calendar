
# Recommended Calendar API Design

This section outlines the recommended RESTful API endpoints and usage for the homerealm Calendar API.

---

## Endpoints

### Calendars

- `GET /calendars` — List all calendars
- `POST /calendars` — Create a new calendar
- `GET /calendars/{id}` — Get calendar details
- `PUT /calendars/{id}` — Update calendar
- `DELETE /calendars/{id}` — Delete calendar

### CalendarEvents

- `GET /calendars/{id}/events` — List events in a calendar
- `GET /events` — List events in a calendar
- `POST /events` — Create event
- `GET /events/{id}` — Get event details
- `PUT /events/{id}` — Update event
- `DELETE /events/{id}` — Delete event


### RecurrenceRules

- `GET /events/{id}/recurrence` — Get recurrence rule
- `GET /recurrence` — Get recurrence rule
- `POST /recurrence` — Add recurrence rule
- `PUT /recurrence/{id}` — Update recurrence rule
- `DELETE /recurrence/{id}` — Delete recurrence rule

### OccurrenceOverrides

- `GET /recurrence/{id}/occurrence-overrides` — List occurrence overrides
- `POST /occurrence-overrides` — Add occurrence override
- `GET /occurrence-overrides` — Get occurrence overrides
- `PUT /occurrence-overrides/{id}` — Update occurrences override
- `DELETE /occurrence-overrides/{id}` — Delete occurrences override

### Attendees

- `GET /events/{id}/attendees` — List attendees
- `POST /attendees` — Add attendees
- `GET /attendees` — Get attendees
- `PUT /attendees/{id}` — Update attendees
- `DELETE /attendees/{id}` — Delete attendees 

### Reminders

- `GET /events/{id}/reminders` — List reminders
- `POST /reminders` — Add attendees
- `GET /reminders` — Get attendees
- `PUT /reminders/{id}` — Update attendees
- `DELETE /reminders/{id}` — Delete attendees 


### Categories

- `POST /categories` — Add Categories
- `GET /categories` — Get Categories
- `PUT /categories/{id}` — Update Categories
- `DELETE /categories/{id}` — Delete Categories 


### Occurrences

- `GET /events/{eventId}/occurrences?start=...&end=...` — List event occurrences (expands RRULE)
- `GET /calendars/{calendarId}/occurrences?start=...&end=...` — List calendar occurrences (expands RRULE)

---

## Notes

- Use filtering and pagination for large lists.
- Occurrences are generated dynamically; only occurrence overrides are stored.
- All endpoints return JSON.

---

See [Resource Relationships](./ResourceRelationships.md) and [Resource to ICAL Conversion](./ResourceToICAL.md) for more details.
