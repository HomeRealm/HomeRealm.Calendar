
# Calendar Service: Vertical Slices by Resource

This document provides a vertical slice for each core Calendar resource, including API endpoints, validation, and entity modeling. Each slice is self-contained and follows Minimal API, FluentValidation, and EF Core best practices.

---

## 1. Calendar Resource Vertical Slice
**Entity:**
- Define `Calendar` entity with properties: Id, Name, Description, OwnerId, etc.
- Configure EF Core DbSet and relationships.


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `CalendarRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `CalendarResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `CalendarValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Create, Get, Update, Delete Calendar (`/api/calendars`).
- Use validator in endpoints; return validation errors as ProblemDetails.

---

## 2. CalendarEvent Resource Vertical Slice
**Entity:**
- Define `CalendarEvent` entity with properties: Id, CalendarId, Title, Start, End, Location, etc.
- Configure EF Core relationships (CalendarEvent → Calendar).


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `CalendarEventRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `CalendarEventResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `CalendarEventValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Create, Get, Update, Delete Event (`/api/calendars/{calendarId}/events`).
- Use validator; return errors as ProblemDetails.

---

## 3. RecurrenceRule Resource Vertical Slice
**Entity:**
- Define `RecurrenceRule` entity (e.g., Frequency, Interval, ByDay, Until, etc.).
- Link to `CalendarEvent` (one-to-many or one-to-one as needed).


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `RecurrenceRuleRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `RecurrenceRuleResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `RecurrenceRuleValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Add, Update, Remove RecurrenceRule (`/api/events/{eventId}/recurrence`).

---

## 4. OccurrenceOverride Resource Vertical Slice
**Entity:**
- Define `OccurrenceOverride` entity (e.g., Id, EventId, Date, OverrideType, PropertiesChanged).
- Link to `CalendarEvent`.


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `OccurrenceOverrideRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `OccurrenceOverrideResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `OccurrenceOverrideValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Add, Update, Remove OccurrenceOverride (`/api/events/{eventId}/occurrence-overrides`).

---

## 5. Attendee Resource Vertical Slice
**Entity:**
- Define `Attendee` entity (e.g., Id, EventId, Name, Email, Status).
- Link to `CalendarEvent`.


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `AttendeeRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `AttendeeResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `AttendeeValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Add, Update, Remove Attendee (`/api/events/{eventId}/attendees`).

---

## 6. Reminder Resource Vertical Slice
**Entity:**
- Define `Reminder` entity (e.g., Id, EventId, TriggerTime, Method).
- Link to `CalendarEvent`.


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `ReminderRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `ReminderResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `ReminderValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Add, Update, Remove Reminder (`/api/events/{eventId}/reminders`).

---

## 7. Category Resource Vertical Slice
**Entity:**
- Define `Category` entity (e.g., Id, Name, Color, Description).
- Link to `CalendarEvent` (many-to-many).


**DTOs:**
- Define a single **Resource Request DTO** (e.g., `CategoryRequest`) for both create and update operations. **Request DTOs must not include the primary key (Id)**.
- Define a **Response DTO** (e.g., `CategoryResponse`) for output, which includes the primary key and all returned fields.

**Validation:**
- Implement validator (e.g., `CategoryValidator`) using FluentValidation for the **entity** (not the DTO).
- In the endpoint, map the Request DTO to the entity, then validate the entity before passing it to the service.

**API:**
- Map endpoints: Add, Update, Remove Category (`/api/categories`).

---


## 1. Occurrence Resources Vertical Slice
**Entity:**

- No Entity: resources are generated upon request.

**DTOs:**

- No Request DTO because it's a get only endpoint
- Define a **Response DTO** (e.g., `CalendarResponse`) for output, which includes the relevant CalendarEventId and all returned fields.

**Implementation Details**

Use ical.net to generate occurrences and then project them to a response dto. **DO NOT** pass the ical.net occurrence directly. 
You can use ical.net's `Calendar.GenerateOccurrences` method to generate a list of Occurrences. 

**API:**

- Map endpoints: Get All Occurrences (`/api/calendars/occurrences`).
- Map endpoints: Get Occurrences for Calendar (`/api/calendars/{id}/occurrences`)
- Map endpoints: Get Occurrences for Calendar Event (`/api/Events/{id}/occurences`)
- All occurrence endpoints have query parameters `start=...&end=...`
- Max range is configurable and defaults to 5 years. 
- Default range is configurable and defaults to 1 year
- configuration is measured in days. 
---

## Common Steps for All Slices

- Register all entities in DbContext.
- Register all validators in DI.
- Ensure every API endpoint uses both a **Request DTO** (without primary key) and a **Response DTO** (with primary key and output fields).
- Write unit tests for validators and endpoints.
- Document endpoints with OpenAPI/Swagger.

---

Each vertical slice ensures a complete, testable, and maintainable implementation for its resource, following best practices for API, validation, and data modeling.
