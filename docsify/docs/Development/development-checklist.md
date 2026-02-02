# Development Checklist

This development checklist makes highlights the common things that is required to create a new page. It is designed to 
make it easier to verify a consistent coding style. 

## UI

* UI is a BFF archiecture and therefore only makes requests back to the HomeRealm service. 
* UI components go into HomeRealm.Shared. This will allow for it to be added into a MAUI project in the future. 
* Use DataAnnotations for basic validation, like required
* Display validation errors returned from API
* Do not use DTO from API for UI. Either use the componment itself or a separate view model. 

## API 
* Use minimal APIs
* Call fluent validation from endpoint
* Do work in the service
* Wrap DbContext in a DataStore for DB interactions
* Unit Test FluentValidation Validators
* Unit Test Service
* APIs must use both **Request DTOs** (for input) and **Response DTOs** (for output).
* **Request DTOs must never include the primary key** (e.g., Id) of the resource. The primary key should only be present in the Response DTO or as a route parameter.
