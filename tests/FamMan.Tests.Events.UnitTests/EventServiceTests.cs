using System.Text.Json;
using FamMan.Api.Events.Dtos;
using FamMan.Api.Events.Entities;
using FamMan.Api.Events.Interfaces;
using FamMan.Api.Events.Services;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace FamMan.Tests.Events.UnitTests;

public class EventServiceTest
{
  private readonly IEventDataStore _dataStore;
  private readonly EventService _sut;

  public EventServiceTest()
  {
    _dataStore = Substitute.For<IEventDataStore>();
    _sut = new EventService(_dataStore);
  }

  [Fact]
  public async Task GetAllEventsAsync_ShouldReturnMappedEvents()
  {
    // Arrange
    var events = new List<ActionableEvent>
      {
        new() { Id = Guid.NewGuid(), Name = "Event 1", EventType = "Type 1", Description = "Desc 1", RecurrenceRules = JsonSerializer.Serialize("{rule: Rule1}"), IsActive = true, CreatedAt = DateTime.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "Event 2", EventType = "Type 2", Description = "Desc 2", RecurrenceRules = JsonSerializer.Serialize("{rule: Rule2}"), IsActive = true, CreatedAt = DateTime.UtcNow },
      };
    _dataStore.GetAllEvents().Returns(events.BuildMock());

    // Act
    var result = await _sut.GetAllEventsAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.Count.ShouldBe(2);
    result[0].Id.ShouldBe(events[0].Id);
    result[0].Name.ShouldBe(events[0].Name);
    result[1].Id.ShouldBe(events[1].Id);
    result[1].Name.ShouldBe(events[1].Name);
  }

  [Fact]
  public async Task GetEventByIdAsync_WhenEventExists_ShouldReturnMappedEvent()
  {
    // Arrange
    var eventId = Guid.NewGuid();
    var eventEntity = new ActionableEvent
    {
      Id = eventId,
      Name = "Test Event",
      EventType = "Test Type",
      Description = "Test Description",
      RecurrenceRules = JsonSerializer.Serialize("{rule: Test Rule}"),
      IsActive = true,
      CreatedAt = DateTime.UtcNow,
    };
    _dataStore.GetEventByIdAsync(eventId, TestContext.Current.CancellationToken).Returns(eventEntity);

    // Act
    var (status, result) = await _sut.GetEventByIdAsync(eventId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.Id.ShouldBe(eventEntity.Id);
    result.Name.ShouldBe(eventEntity.Name);
    result.EventType.ShouldBe(eventEntity.EventType);
    result.Description.ShouldBe(eventEntity.Description);
    result.RecurrenceRules.GetString().ShouldBe(JsonSerializer.Deserialize<JsonElement>(eventEntity.RecurrenceRules).GetString());
    result.IsActive.ShouldBe(eventEntity.IsActive);
    result.CreatedAt.ShouldBe(eventEntity.CreatedAt);
  }

  [Fact]
  public async Task GetEventByIdAsync_WhenEventDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var eventId = Guid.NewGuid();
    _dataStore.GetEventByIdAsync(eventId, TestContext.Current.CancellationToken).Returns((ActionableEvent?)null);

    // Act
    var (status, result) = await _sut.GetEventByIdAsync(eventId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task CreateEventAsync_ShouldCreateAndReturnMappedEvent()
  {
    // Arrange
    var serializedRule = JsonSerializer.Serialize("{rule: New Rule}");

    var eventDto = new ActionableEventDto
    {
      Id = Guid.NewGuid(),
      Name = "New Event",
      EventType = "New Type",
      Description = "New Description",
      RecurrenceRules = JsonSerializer.Deserialize<JsonElement>(serializedRule),
      CreatedAt = DateTime.UtcNow,
    };
    var createdEvent = new ActionableEvent
    {
      Id = eventDto.Id,
      Name = eventDto.Name,
      EventType = eventDto.EventType,
      Description = eventDto.Description,
      RecurrenceRules = JsonSerializer.Serialize(eventDto.RecurrenceRules),
      CreatedAt = eventDto.CreatedAt,
    };
    _dataStore.CreateEventAsync(Arg.Any<ActionableEvent>(), TestContext.Current.CancellationToken).Returns(createdEvent);

    // Act
    var result = await _sut.CreateEventAsync(eventDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.Id.ShouldBe(eventDto.Id);
    result.Name.ShouldBe(eventDto.Name);
    result.Description.ShouldBe(eventDto.Description);
    await _dataStore.Received(1).CreateEventAsync(Arg.Is<ActionableEvent>(e =>
      e.Name == eventDto.Name &&
      e.Description == eventDto.Description), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateEventAsync_WhenEventExists_ShouldUpdateAndReturnEvent()
  {
    // Arrange
    var eventId = Guid.NewGuid();
    var oldSerializedRule = JsonSerializer.Serialize("{rule: Old Rule}");
    var updatedSerializedRule = JsonSerializer.Serialize("{rule: Updated Rule}");

    var existingEvent = new ActionableEvent
    {
      Id = eventId,
      Name = "Old Name",
      EventType = "Old Type",
      Description = "Old Description",
      RecurrenceRules = oldSerializedRule,
      CreatedAt = DateTime.UtcNow.AddDays(-5),
    };
    var eventDto = new ActionableEventDto
    {
      Id = eventId,
      Name = "Updated Name",
      EventType = "Updated Type",
      Description = "Updated Description",
      RecurrenceRules = JsonSerializer.Deserialize<JsonElement>(updatedSerializedRule),
      CreatedAt = existingEvent.CreatedAt,
      UpdatedAt = DateTime.UtcNow
    };
    var modifiedEvent = new ActionableEvent
    {
      Id = eventId,
      Name = eventDto.Name,
      EventType = eventDto.EventType,
      Description = eventDto.Description,
      RecurrenceRules = JsonSerializer.Serialize(eventDto.RecurrenceRules),
      CreatedAt = eventDto.CreatedAt,
      UpdatedAt = eventDto.UpdatedAt
    };
    _dataStore.GetEventByIdAsync(eventId, TestContext.Current.CancellationToken).Returns(existingEvent);
    _dataStore.UpdateEventAsync(existingEvent, Arg.Any<ActionableEvent>(), TestContext.Current.CancellationToken).Returns(modifiedEvent);

    // Act
    var (status, updatedEvent) = await _sut.UpdateEventAsync(eventDto, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    updatedEvent.ShouldNotBeNull();
    updatedEvent.Name.ShouldBe(eventDto.Name);
    updatedEvent.Description.ShouldBe(eventDto.Description);
    updatedEvent.UpdatedAt.ShouldBe(eventDto.UpdatedAt);
    await _dataStore.Received(1).UpdateEventAsync(existingEvent, Arg.Any<ActionableEvent>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateEventAsync_WhenEventDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var eventId = Guid.NewGuid();
    var serializedRule = JsonSerializer.Serialize("{rule: Updated Rule}");

    var eventDto = new ActionableEventDto
    {
      Id = eventId,
      Name = "Updated Name",
      EventType = "Updated Type",
      Description = "Updated Description",
      RecurrenceRules = JsonSerializer.Deserialize<JsonElement>(serializedRule),
      CreatedAt = DateTime.UtcNow
    };
    _dataStore.GetEventByIdAsync(eventId, TestContext.Current.CancellationToken).Returns((ActionableEvent?)null);

    // Act
    var (status, actionableEvent) = await _sut.UpdateEventAsync(eventDto, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    actionableEvent.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateEventAsync(Arg.Any<ActionableEvent>(), Arg.Any<ActionableEvent>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteEvent_ShouldCallDataStoreDelete()
  {
    // Arrange
    var eventId = Guid.NewGuid();

    // Act
    await _sut.DeleteEventAsync(eventId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteEventAsync(eventId, TestContext.Current.CancellationToken);
  }
}
