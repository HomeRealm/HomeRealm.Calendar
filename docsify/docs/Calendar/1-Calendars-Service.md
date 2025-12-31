# FamMan Calendar

## Understanding the Calendar API

The Calendar API is a core service within the FamMan family management system. Its main purpose is to organize and manage all events that families need to keep track of—such as appointments, chores, birthdays, and holidays—making it easier for everyone to stay coordinated.

## Purpose

The Calendar API exists to centralize all calendar-related information for a family. Events can be added to specific calendars—such as chores, birthdays, appointments, or even a parents-only calendar—allowing each family member to view only the events that matter to them. For example, a parent might want to see just the chores calendar, while another family member checks birthdays or appointments. At the same time, all family events can be displayed together in one unified view, making it easy to coordinate and avoid conflicts. By providing a single source of truth for events, the Calendar API helps families avoid missed appointments and overlapping schedules, and ensures that everyone has access to the same up-to-date information.

## Key Features

- **Unified Event Management:** Collects and manages all types of family events in one place.
- **Automatic Scheduling:** Generates event occurrences, including repeating events, so schedules are always accurate.
- **Broad Compatibility:** Supports both direct API access and the widely-used ICAL format, making it easy to connect with many calendar applications.
- **Reliable Technology:** Built using modern .NET technology and the ical.net library for robust, standards-compliant calendar data.

## Role in FamMan

Within FamMan, the Calendar API acts as the hub for all scheduling needs. Other services—like the Chores and Events APIs—rely on it to display and manage their scheduled items. This integration ensures that every family activity, from daily chores to special events, appears on a single, shared calendar.


## Technical Implementation

The Calendar API is implemented using modern .NET Minimal APIs, following a vertical slice architecture for each resource (e.g., Calendar, Event, Attendee). Key technical patterns include:

- **Request/Response DTOs:** Every API endpoint uses a Request DTO (for input, without primary key) and a Response DTO (for output, including primary key and all returned fields).
- **Entity Validation:** FluentValidation is applied to the entity itself, not the DTO. The DTO is mapped to the entity in the endpoint and validated before being passed to the service.
- **Vertical Slice Architecture:** Each resource is implemented as a self-contained slice, including its entity, DTOs, validation, and endpoints, ensuring maintainability and testability.

For detailed development steps and user stories, see [calendar-service-user-stories.md](calendar-service-user-stories.md).

