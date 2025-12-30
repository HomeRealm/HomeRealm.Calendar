# Welcome to FamMan Documentation

## What is FamMan?

**FamMan** (pronounced like "famine"; short for *family management*) is a modular application designed to help manage different aspects of family life. It leverages a microservices architecture, allowing each functional area to operate as an independent, reusable service.

### Key Features

- **Modular Microservices:** Each API (currently Chores and Events) is implemented as a standalone microservice. These can be integrated into other systems or extended with additional services as needed.
- **Modern UI:** The user interface is built using Blazor WebAssembly, providing a rich, interactive web experience.
- **BFF Architecture:** The UI communicates with backend services through a Backend-for-Frontend (BFF) pattern, ensuring secure and efficient data flow.
- **Extensible Design:** New family management modules can be added as separate microservices, making FamMan highly adaptable.

FamMan aims to provide a flexible, scalable foundation for managing chores, events, and other family-related activities, with a focus on integration and ease of use.