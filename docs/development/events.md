# Events API - Microservice Overview

## Purpose

The Events API is a microservice designed to manage time-based actions and notifications within the FamMan application. It provides a robust system for scheduling, managing, and delivering events at specified times through webhook-based notifications.

## Core Functionality

### Event Management

The Events API allows applications to create and manage scheduled events that trigger automated actions at specified times. Each event is a self-contained unit that executes based on defined temporal rules.

### Time-Based Execution

When an event's scheduled time occurs, the service automatically publishes the event through a webhook mechanism, enabling decoupled, asynchronous communication between services.

## Event Structure

Creating an event requires the following components:

### Required Fields

- **Name**: A descriptive identifier for the event
- **Type**: The category or classification of the event
- **Description**: Detailed information about the event's purpose and behavior
- **Recurrence Rules**: Configuration for recurring events, defining when and how often the event should trigger
- **Payload**: The data package to be delivered when the event fires

### Example Events

#### Birthday Reminder

```json
{
  "name": "Birthday Reminder",
  "type": "notification.reminder",
  "description": "Annual reminder for family member birthday",
  "recurrenceRules": {
    "frequency": "yearly",
    "month": 6,
    "day": 15
  },
  "payload": {
    "memberId": "12345",
    "message": "Don't forget John's birthday tomorrow!",
    "notificationChannels": ["email", "sms"]
  }
}
```

#### Monthly Recurring Payment

```json
{
  "name": "Mortgage Payment",
  "type": "finance.recurring_payment",
  "description": "Monthly mortgage payment deduction from checking account",
  "recurrenceRules": {
    "frequency": "monthly",
    "dayOfMonth": 1,
    "time": "09:00:00"
  },
  "payload": {
    "accountId": "checking-001",
    "transactionType": "debit",
    "amount": 2450.00,
    "currency": "USD",
    "category": "housing",
    "payee": "First National Bank",
    "description": "Monthly mortgage payment",
    "metadata": {
      "loanNumber": "MTG-789456",
      "principalAmount": 1850.00,
      "interestAmount": 450.00,
      "escrowAmount": 150.00
    }
  }
}
```

## Webhook Delivery

### CloudEvents Compliance

The Events API follows the [CloudEvents HTTP Webhook specification](https://github.com/cloudevents/spec/blob/main/cloudevents/http-webhook.md) to ensure standardized, interoperable event delivery.

### Key Features

#### Secure Delivery
- **HTTPS Required**: All webhook deliveries use HTTP-over-TLS (HTTPS) for secure communication
- **POST Method**: Events are delivered using HTTP POST requests
- **Content-Type Header**: All requests include appropriate Content-Type headers

#### Authorization
The service supports two authorization methods:
1. **Authorization Header**: OAuth 2.0 Bearer tokens in the Authorization header (preferred)
2. **URI Query Parameter**: Access token as a query parameter (legacy support)

#### Abuse Protection
To prevent misuse and ensure legitimate delivery:
- **Validation Handshake**: Uses HTTP OPTIONS requests to verify webhook endpoints
- **WebHook-Request-Origin**: Identifies the sending system
- **WebHook-Request-Callback**: Optional callback URL for asynchronous permission grants
- **Rate Limiting**: Configurable request rates with WebHook-Request-Rate headers

#### Response Handling
- **200 OK / 201 Created**: Successful processing with details
- **202 Accepted**: Event accepted but not yet processed
- **204 No Content**: Successful processing without response payload
- **410 Gone**: Endpoint retired, stops further deliveries
- **429 Too Many Requests**: Rate limit exceeded, includes Retry-After header
- **415 Unsupported Media Type**: Unrecognized event format

### Delivery Protocol

1. **Event Trigger**: When an event's scheduled time arrives
2. **Webhook POST**: The service sends an HTTPS POST request to the registered webhook URL
3. **Authorization**: Includes Bearer token or query parameter for authentication
4. **Payload Delivery**: Event payload is transmitted in the request body
5. **Response Processing**: Handles the webhook response according to status codes
6. **Retry Logic**: Automatically retries failed deliveries based on response codes

## Use Cases

- **Recurring Reminders**: Birthday notifications, anniversary alerts, bill payment reminders
- **Scheduled Tasks**: Automated reports, periodic data synchronization, maintenance windows
- **Deadline Management**: Task due dates, project milestones, appointment reminders
- **Event Notifications**: Calendar events, family gatherings, scheduled activities

## Architecture Benefits

### Microservice Design
- **Decoupled**: Independent service focused solely on event management
- **Scalable**: Can handle high volumes of events and webhook deliveries
- **Reliable**: Built-in retry mechanisms and error handling

### Standards Compliance
- **Interoperable**: CloudEvents compliance ensures compatibility with other systems
- **Well-Documented**: Follows established specifications and best practices
- **Secure**: Built-in security features including HTTPS, authorization, and abuse protection

## Integration

Applications integrate with the Events API by:
1. **Creating Events**: POST requests to create new scheduled events
2. **Registering Webhooks**: Configure endpoint URLs to receive event notifications
3. **Handling Deliveries**: Implement webhook endpoints following CloudEvents specifications
4. **Managing Lifecycle**: Update, delete, or pause events as needed

## Technical Specifications

- **Protocol**: HTTP 1.1 / HTTP/2
- **Security**: HTTPS (TLS), OAuth 2.0 Bearer tokens
- **Specification**: CloudEvents HTTP Webhook v1.0.3
- **Delivery Method**: HTTP POST with configurable retry logic
- **Rate Limiting**: Configurable per-endpoint request rates
