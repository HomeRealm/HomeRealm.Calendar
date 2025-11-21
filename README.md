# FamMan - Family Manager

The home management application to keep the family man from famine.

## About

FamMan is a comprehensive family management application designed to help families organize their daily lives, manage events, and coordinate activities. Built with modern .NET technologies, it provides a seamless experience across web platforms.

## Architecture

This project uses a microservices architecture with .NET Aspire orchestration, implementing a Backend-for-Frontend (BFF) pattern:

- **FamMan** - Backend-for-Frontend (BFF) server. This is the primary backend that hosts the Blazor WebAssembly application and acts as an API gateway. All client requests go through FamMan, which is responsible for orchestrating calls to downstream microservices, aggregating data, and managing authentication/authorization. This pattern provides a unified API surface for the client while keeping backend service complexity hidden.
- **FamMan.Client** - Blazor WebAssembly client application. Communicates exclusively with the FamMan BFF server and has no direct knowledge of backend microservices.
- **FamMan.Api.Events** - Events API microservice for managing family events and activities
- **FamMan.AppHost** - .NET Aspire orchestrator for local development and service coordination
- **FamMan.ServiceDefaults** - Shared service configuration and extensions

## Technology Stack

- .NET 10.0
- Blazor WebAssembly
- ASP.NET Core Web APIs
- .NET Aspire for orchestration
- Docker support

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerized services)

### Development Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/gorrilla10101/famman.git
   cd famman
   ```

2. **Run the application using .NET Aspire**
   
   Navigate to the AppHost project and run:
   ```bash
   cd src/FamMan.AppHost
   dotnet run
   ```
   
   This will start all services (web app and APIs) through the .NET Aspire orchestrator. The Aspire dashboard will open automatically, showing all running services and their endpoints.

3. **Alternative: Run individual projects**
   
   To run just the main web application:
   ```bash
   cd src/FamMan/FamMan
   dotnet run
   ```
   
   To run the Events API:
   ```bash
   cd src/FamMan.Api.Events
   dotnet run
   ```

### Development in Visual Studio

1. Open `src/FamMan.slnx` in Visual Studio 2022
2. Set `FamMan.AppHost` as the startup project
3. Press F5 to run

The .NET Aspire dashboard will launch, providing a unified view of all services, logs, and telemetry.

## Project Structure

```
src/
├── FamMan/
│   ├── FamMan/              # Server-side Blazor host
│   └── FamMan.Client/       # Client-side Blazor WebAssembly
├── FamMan.Api.Events/       # Events microservice API
├── FamMan.AppHost/          # .NET Aspire orchestration
└── FamMan.ServiceDefaults/  # Shared service configurations
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the terms specified in the LICENSE file. 
