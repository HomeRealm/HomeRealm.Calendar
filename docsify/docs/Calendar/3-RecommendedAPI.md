
# Recommended Calendar API Design

This section outlines the recommended RESTful API endpoints and usage for the FamMan Calendar API.

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
- `POST /calendars/{id}/events` — Create event
- `GET /events/{id}` — Get event details
- `PUT /events/{id}` — Update event
- `DELETE /events/{id}` — Delete event


### RecurrenceRules

- `GET /events/{id}/recurrence` — Get recurrence rule
- `POST /events/{id}/recurrence` — Add recurrence rule
- `PUT /recurrence/{id}` — Update recurrence rule
- `DELETE /recurrence/{id}` — Delete recurrence rule

### OccurrenceOverrides

- `POST /recurrence/{id}/occurrence-overrides` — Add occurrence override
- `GET /recurrence/{id}/occurrence-overrides` — List occurrence overrides

### Attendees

- `POST /events/{id}/attendees` — Add attendee
- `GET /events/{id}/attendees` — List attendees

### Reminders

- `POST /events/{id}/reminders` — Add reminder
- `GET /events/{id}/reminders` — List reminders

### Categories

- `GET /categories` — List categories
- `POST /categories` — Create category

### Occurrences

- `GET /events/{id}/occurrences?start=...&end=...` — List event occurrences (expands RRULE)

---

## Notes

- Use filtering and pagination for large lists.
- Occurrences are generated dynamically; only occurrence overrides are stored.
- All endpoints return JSON.

---

See [Resource Relationships](./ResourceRelationships.md) and [Resource to ICAL Conversion](./ResourceToICAL.md) for more details.
