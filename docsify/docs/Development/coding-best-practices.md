# Coding Best Practices

This document outlines the coding standards and best practices for the HomeRealm application.

## General Principles

- **SOLID Principles**: Follow SOLID design principles for maintainable and scalable code
- **DRY (Don't Repeat Yourself)**: Avoid code duplication by extracting reusable components and utilities
- **KISS (Keep It Simple, Stupid)**: Favor simple, readable solutions over complex ones
- **Separation of Concerns**: Keep business logic separate from presentation and data access layers
- **Consistent Naming**: Use clear, descriptive names that follow .NET conventions

## Blazor Best Practices

### Component Design

- **Single Responsibility**: Each component should have one clear purpose
- **Keep Components Small**: Break large components into smaller, reusable child components
- **Component Parameters**: Use `[Parameter]` for component inputs and validate them in 
`OnParametersSet()`

```csharp
[Parameter, EditorRequired]
public string Title { get; set; } = string.Empty;

protected override void OnParametersSet()
{
	ArgumentException.ThrowIfNullOrEmpty(Title);
}
```

### State Management

- **Cascading Values**: Use `CascadingValue` and `CascadingParameter` for data that needs to flow down the component tree
- **Component State**: Keep component state minimal and local when possible
- **Event Callbacks**: Use `EventCallback<T>` for parent-child component communication

```csharp
[Parameter]
public EventCallback<string> OnItemSelected { get; set; }

private async Task HandleSelection(string item)
{
	await OnItemSelected.InvokeAsync(item);
}
```

### Lifecycle Methods

- **Use Appropriate Lifecycle Methods**: 
  - `OnInitialized()/OnInitializedAsync()` - One-time initialization
  - `OnParametersSet()/OnParametersSetAsync()` - React to parameter changes
  - `OnAfterRender()/OnAfterRenderAsync()` - DOM interactions (use `firstRender` parameter)
- **Dispose Resources**: Implement `IDisposable` or `IAsyncDisposable` to clean up subscriptions, timers, and event handlers

```csharp
@implements IAsyncDisposable

@code {
	private IDisposable? subscription;

	protected override void OnInitialized()
	{
		subscription = EventService.Subscribe(HandleEvent);
	}

	public async ValueTask DisposeAsync()
	{
		subscription?.Dispose();
		await Task.CompletedTask;
	}
}
```

### Performance

- **Virtualization**: Use `Virtualize<T>` component for large lists
- **Streaming Rendering**: Leverage streaming rendering for async data loading
- **Avoid Unnecessary Re-renders**: Use `ShouldRender()` to control when components update
- **Use `@key`**: Always use `@key` directive in loops to help Blazor track elements

```razor
@foreach (var item in Items)
{
	<ItemComponent @key="item.Id" Item="@item" />
}
```

### Error Handling

- **Error Boundaries**: Use `ErrorBoundary` component to catch and handle component errors gracefully

```razor
<ErrorBoundary>
	<ChildContent>
		@Body
	</ChildContent>
	<ErrorContent Context="exception">
		<ErrorDisplay Exception="@exception" />
	</ErrorContent>
</ErrorBoundary>
```

### Forms and Validation

- **EditForm**: Always use `EditForm` with `EditContext` or model binding
- **Data Annotations**: Use data annotations for basic validation in UI
- **Fluent Validation**: Use FluentValidation for complex validation scenarios against the API (see Fluent Validation section)
- **Validation Summary**: Include `ValidationSummary` or `ValidationMessage` components

## Minimal API Best Practices

### Endpoint Organization

- **Group Related Endpoints**: Use `MapGroup()` to organize related endpoints

```csharp
var eventsGroup = app.MapGroup("/api/events")
	.WithTags("Events")
	.WithOpenApi();

eventsGroup.MapGet("/", GetAllEvents);
eventsGroup.MapGet("/{id}", GetEventById);
eventsGroup.MapPost("/", CreateEvent);
```

### Route Handlers

- **Keep Handlers Thin**: Extract business logic into services
- **Use Dependency Injection**: Inject services directly into route handlers

```csharp
app.MapGet("/api/events/{id}", async (
	int id,
	IEventService eventService,
	CancellationToken ct) =>
{
	var result = await eventService.GetEventByIdAsync(id, ct);
	return result is not null ? Results.Ok(result) : Results.NotFound();
});
```

### Request/Response Patterns

- **Use Typed Results**: Leverage `TypedResults` for type-safe responses

```csharp
app.MapGet("/api/events/{id}", async Task<Results<Ok<EventDto>, NotFound>> (
	int id,
	IEventService eventService) =>
{
	var result = await eventService.GetEventByIdAsync(id);
	return result is not null 
		? TypedResults.Ok(result) 
		: TypedResults.NotFound();
});
```

- **DTO Pattern**: Use Data Transfer Objects for API contracts
- **Result Pattern**: Consider using Result<T> pattern for operation outcomes

### Validation

- **Validate at the Endpoint Level**: Perform validation directly in the endpoint handler for clarity and explicitness
- **Inject Validators**: Use FluentValidation validators injected via DI
- **Return Problem Details**: Use `Results.ValidationProblem()` for consistent error responses

```csharp
app.MapPost("/api/events", async (
	CreateEventRequest request,
	IValidator<CreateEventRequest> validator,
	IEventService eventService) =>
{
	var validationResult = await validator.ValidateAsync(request);
    
	if (!validationResult.IsValid)
	{
		return Results.ValidationProblem(validationResult.ToDictionary());
	}
    
	var result = await eventService.CreateEventAsync(request);
	return Results.Created($"/api/events/{result.Id}", result);
});
```

- **Extension Method Pattern**: For reducing repetition across endpoints, create extension methods on `IValidator<T>`

```csharp
public static class ValidatorExtensions
{
	public static async Task<(bool IsValid, IResult? Problem)> ValidateWithResultAsync<T>(
		this IValidator<T> validator,
		T instance,
		CancellationToken cancellationToken = default)
	{
		var result = await validator.ValidateAsync(instance, cancellationToken);
        
		if (!result.IsValid)
		{
			return (false, Results.ValidationProblem(result.ToDictionary()));
		}
        
		return (true, null);
	}
}

// Usage
app.MapPost("/api/events", async (
	CreateEventRequest request,
	IValidator<CreateEventRequest> validator,
	IEventService eventService,
	CancellationToken ct) =>
{
	var (isValid, problem) = await validator.ValidateWithResultAsync(request, ct);
	if (!isValid) return problem!;
    
	var result = await eventService.CreateEventAsync(request, ct);
	return Results.Created($"/api/events/{result.Id}", result);
});
```

This approach is cleaner because:
- The extension is on the validator interface, making it more discoverable
- It accepts the DTO as a parameter, following standard validation patterns
- It supports `CancellationToken` for proper async operations
- It returns `IResult` which is more flexible than the specific `ValidationProblem` type

### Error Handling

- **Global Exception Handler**: Use `IExceptionHandler` for centralized error handling
- **Problem Details**: Return RFC 7807 Problem Details for errors

```csharp
public class GlobalExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Title = "An error occurred",
			Detail = exception.Message
		};

		httpContext.Response.StatusCode = 
			StatusCodes.Status500InternalServerError;
            
		await httpContext.Response
			.WriteAsJsonAsync(problemDetails, cancellationToken);
            
		return true;
	}
}
```

### Documentation

- **OpenAPI/Swagger**: Use `.WithOpenApi()` to document endpoints
- **Descriptive Summaries**: Add descriptions and examples

```csharp
.MapGet("/api/events/{id}", GetEventById)
.WithName("GetEventById")
.WithSummary("Retrieves a specific event by ID")
.WithDescription("Returns the event details if found, otherwise 404")
.Produces<EventDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);
```

### Security

- **Authorization**: Apply authorization policies to endpoints

```csharp
app.MapGet("/api/events", GetEvents)
	.RequireAuthorization("EventsRead");
```

- **Rate Limiting**: Implement rate limiting for API endpoints
- **CORS**: Configure CORS appropriately for your client applications

## Fluent Validation Best Practices

### Validator Organization

- **One Validator Per Model**: Create a dedicated validator class for each model/DTO
- **Naming Convention**: Use `{ModelName}Validator` naming pattern

```csharp
public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
	public CreateEventRequestValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(200);
            
		RuleFor(x => x.StartDate)
			.NotEmpty()
			.GreaterThan(DateTime.UtcNow);
	}
}
```

### Rule Definition

- **Fluent Chains**: Chain validation rules for readability
- **Custom Messages**: Provide clear, user-friendly error messages

```csharp
RuleFor(x => x.Email)
	.NotEmpty().WithMessage("Email is required")
	.EmailAddress().WithMessage("Invalid email format")
	.MaximumLength(255).WithMessage("Email must not exceed 255 characters");
```

- **When Conditions**: Use `When()` for conditional validation

```csharp
RuleFor(x => x.EndDate)
	.GreaterThan(x => x.StartDate)
	.When(x => x.EndDate.HasValue)
	.WithMessage("End date must be after start date");
```

### Complex Validation

- **Custom Rules**: Create custom validation rules for complex logic

```csharp
RuleFor(x => x.PhoneNumber)
	.Must(BeAValidPhoneNumber)
	.WithMessage("Phone number is not valid");

private bool BeAValidPhoneNumber(string? phoneNumber)
{
	if (string.IsNullOrEmpty(phoneNumber)) return true;
	return Regex.IsMatch(phoneNumber, @"^\+?[1-9]\d{1,14}$");
}
```

- **Async Validation**: Use `MustAsync()` for async validation (e.g., database checks)

```csharp
RuleFor(x => x.Email)
	.MustAsync(BeUniqueEmail)
	.WithMessage("Email already exists");

private async Task<bool> BeUniqueEmail(
	string email,
	CancellationToken cancellationToken)
{
	return !await _userRepository.EmailExistsAsync(email, cancellationToken);
}
```

### Validator Composition

- **Include Nested Validators**: Validate child objects with `SetValidator()`

```csharp
public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
	public CreateEventRequestValidator()
	{
		RuleFor(x => x.Location)
			.SetValidator(new LocationValidator());
            
		RuleForEach(x => x.Attendees)
			.SetValidator(new AttendeeValidator());
	}
}
```

### Dependency Injection

- **Register Validators**: Register all validators in DI container

```csharp
services.AddValidatorsFromAssemblyContaining<CreateEventRequestValidator>();
```

- **Inject Dependencies**: Validators can have their own dependencies

```csharp
public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
	private readonly IEventRepository _repository;
    
	public CreateEventRequestValidator(IEventRepository repository)
	{
		_repository = repository;
        
		RuleFor(x => x.CategoryId)
			.MustAsync(CategoryExists)
			.WithMessage("Category does not exist");
	}
    
	private async Task<bool> CategoryExists(int categoryId, CancellationToken ct)
	{
		return await _repository.CategoryExistsAsync(categoryId, ct);
	}
}
```

### Integration with Minimal APIs

- **Inject Validator into Endpoint**: Validate the request at the start of each endpoint handler
- **Consistent Error Response**: Always use `Results.ValidationProblem()` for validation failures
- **Early Return Pattern**: Return validation errors immediately before proceeding with business logic

```csharp
app.MapPost("/api/events", async (
	CreateEventRequest request,
	IValidator<CreateEventRequest> validator,
	IEventService eventService) =>
{
	var validationResult = await validator.ValidateAsync(request);
    
	if (!validationResult.IsValid)
	{
		return Results.ValidationProblem(validationResult.ToDictionary());
	}
    
	var result = await eventService.CreateEventAsync(request);
	return Results.Created($"/api/events/{result.Id}", result);
})
.WithName("CreateEvent")
.Produces<EventDto>(StatusCodes.Status201Created)
.ProducesValidationProblem();
```

### Testing

- **Test Validators**: Write unit tests for your validators

```csharp
[Fact]
public async Task Should_Have_Error_When_Title_Is_Empty()
{
	var validator = new CreateEventRequestValidator();
	var model = new CreateEventRequest { Title = "" };
    
	var result = await validator.TestValidateAsync(model);
    
	result.ShouldHaveValidationErrorFor(x => x.Title);
}
```

## Code Review Checklist

- [ ] Code follows SOLID principles
- [ ] Components are small and focused
- [ ] Proper use of lifecycle methods
- [ ] Resources are disposed properly
- [ ] Validation is implemented using FluentValidation
- [ ] API endpoints return appropriate status codes
- [ ] Error handling is implemented
- [ ] Code is documented with XML comments where appropriate
- [ ] Unit tests are written for business logic
- [ ] No sensitive data is logged or exposed

## Additional Resources

- [Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor/)
- [Minimal APIs Documentation](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)
- [.NET API Design Guidelines](https://learn.microsoft.com/dotnet/standard/design-guidelines/)
