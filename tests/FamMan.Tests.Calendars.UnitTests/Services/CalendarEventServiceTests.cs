using FamMan.Api.Calendars.Dtos.CalendarEvent;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.CalendarEvent;
using FamMan.Api.Calendars.Services.CalendarEvent;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace FamMan.Tests.Calendars.UnitTests.Services;

public class CalendarEventServiceTests
{
  private readonly ICalendarEventDataStore _dataStore;
  private readonly CalendarEventService _sut;

  public CalendarEventServiceTests()
  {
    _dataStore = Substitute.For<ICalendarEventDataStore>();
    _sut = new CalendarEventService(_dataStore);
  }

  [Fact]
  public async Task GetCalendarEventAsync_WhenCalendarEventExists_ShouldReturnMappedCalendarEvent()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var calendarEvent = new CalendarEventEntity
    {
      Id = Guid.NewGuid(),
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };
    _dataStore.GetCalendarEventAsync(calendarEvent.Id, TestContext.Current.CancellationToken).Returns(calendarEvent);

    // Act
    var (status, result) = await _sut.GetCalendarEventAsync(calendarEvent.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarEventResponseDto>();
    result.Id.ShouldBe(calendarEvent.Id);
    result.Title.ShouldBe(calendarEvent.Title);
  }

  [Fact]
  public async Task GetCalendarEventAsync_WhenCalendarEventDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var calendarEventId = Guid.NewGuid();
    _dataStore.GetCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken).Returns((CalendarEventEntity?)null);

    // Act
    var (status, result) = await _sut.GetCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllCalendarEventsAsync_ShouldReturnListOfMappedCalendarEvents()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var calendarEvents = new List<CalendarEventEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        CalendarId = Guid.NewGuid(),
        Title = "Meeting 1",
        Description = "Team Meeting",
        Start = now,
        End = now.AddHours(1),
        Location = "Conference Room",
        AllDay = false,
        RecurrenceId = Guid.NewGuid(),
        CategoryId = Guid.NewGuid(),
        LinkedResource = ""
      },
      new()
      {
        Id = Guid.NewGuid(),
        CalendarId = Guid.NewGuid(),
        Title = "Meeting 2",
        Description = "One-on-one",
        Start = now.AddDays(1),
        End = now.AddDays(1).AddMinutes(30),
        Location = "Office",
        AllDay = false,
        RecurrenceId = Guid.NewGuid(),
        CategoryId = null,
        LinkedResource = ""
      }
    };

    _dataStore.GetAllCalendarEventsAsync(TestContext.Current.CancellationToken).Returns(calendarEvents.BuildMock());

    // Act
    var result = await _sut.GetAllCalendarEventsAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<CalendarEventResponseDto>();
    result[0].Id.ShouldBe(calendarEvents[0].Id);
    result[1].Id.ShouldBe(calendarEvents[1].Id);
  }

  [Fact]
  public async Task CreateCalendarEventAsync_ShouldCreateAndReturnMappedCalendarEvent()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var calendarId = Guid.NewGuid();
    var recurrenceId = Guid.NewGuid();
    var categoryId = Guid.NewGuid();
    var calendarEventRequestDto = new CalendarEventRequestDto
    {
      CalendarId = calendarId,
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = recurrenceId,
      CategoryId = categoryId,
      LinkedResource = ""
    };
    var createdCalendarEvent = new CalendarEventEntity
    {
      Id = Guid.CreateVersion7(),
      CalendarId = calendarId,
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = recurrenceId,
      CategoryId = categoryId,
      LinkedResource = ""
    };
    _dataStore.CreateCalendarEventAsync(Arg.Any<CalendarEventEntity>(), TestContext.Current.CancellationToken).Returns(createdCalendarEvent);

    // Act
    var result = await _sut.CreateCalendarEventAsync(calendarEventRequestDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarEventResponseDto>();
    result.Id.ShouldBe(createdCalendarEvent.Id);
    result.Title.ShouldBe(calendarEventRequestDto.Title);
    await _dataStore
      .Received(1)
      .CreateCalendarEventAsync(
        Arg.Is<CalendarEventEntity>(ce =>
          ce.CalendarId == calendarEventRequestDto.CalendarId &&
          ce.Title == calendarEventRequestDto.Title
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateCalendarEventAsync_WhenCalendarEventExists_ShouldUpdateAndReturnMappedCalendarEvent()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var calendarEventId = Guid.CreateVersion7();
    var calendarId = Guid.NewGuid();
    var recurrenceId = Guid.NewGuid();
    var existingCalendarEvent = new CalendarEventEntity
    {
      Id = calendarEventId,
      CalendarId = calendarId,
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = recurrenceId,
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };
    var calendarEventRequestDto = new CalendarEventRequestDto
    {
      CalendarId = calendarId,
      Title = "Updated Meeting",
      Description = "Updated Description",
      Start = now,
      End = now.AddHours(2),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = recurrenceId,
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };
    var updatedCalendarEvent = new CalendarEventEntity
    {
      Id = calendarEventId,
      CalendarId = calendarEventRequestDto.CalendarId,
      Title = calendarEventRequestDto.Title,
      Description = calendarEventRequestDto.Description,
      Start = calendarEventRequestDto.Start,
      End = calendarEventRequestDto.End,
      Location = calendarEventRequestDto.Location,
      AllDay = calendarEventRequestDto.AllDay,
      RecurrenceId = calendarEventRequestDto.RecurrenceId,
      CategoryId = calendarEventRequestDto.CategoryId,
      LinkedResource = calendarEventRequestDto.LinkedResource
    };
    _dataStore.GetCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken).Returns(existingCalendarEvent);
    _dataStore.UpdateCalendarEventAsync(existingCalendarEvent, Arg.Any<CalendarEventEntity>(), TestContext.Current.CancellationToken).Returns(updatedCalendarEvent);

    // Act
    var (status, result) = await _sut.UpdateCalendarEventAsync(calendarEventRequestDto, calendarEventId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarEventResponseDto>();
    result.Title.ShouldBe(calendarEventRequestDto.Title);
    await _dataStore.Received(1).GetCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateCalendarEventAsync_WhenCalendarEventDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var calendarEventId = Guid.NewGuid();
    var calendarEventRequestDto = new CalendarEventRequestDto
    {
      CalendarId = Guid.NewGuid(),
      Title = "Meeting",
      Description = "Team Meeting",
      Start = now,
      End = now.AddHours(1),
      Location = "Conference Room",
      AllDay = false,
      RecurrenceId = Guid.NewGuid(),
      CategoryId = Guid.NewGuid(),
      LinkedResource = ""
    };
    _dataStore.GetCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken).Returns((CalendarEventEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateCalendarEventAsync(calendarEventRequestDto, calendarEventId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateCalendarEventAsync(Arg.Any<CalendarEventEntity>(), Arg.Any<CalendarEventEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteCalendarEventAsync_ShouldCallDataStore()
  {
    // Arrange
    var calendarEventId = Guid.NewGuid();

    // Act
    await _sut.DeleteCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteCalendarEventAsync(calendarEventId, TestContext.Current.CancellationToken);
  }
}
