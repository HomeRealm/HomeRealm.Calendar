using FamMan.Api.Calendars.Dtos.Calendar;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Calendar;
using FamMan.Api.Calendars.Services.Calendar;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace FamMan.Tests.Calendars.UnitTests;

public class CalendarServiceTests
{
  private readonly ICalendarDataStore _dataStore;
  private readonly CalendarService _sut;

  public CalendarServiceTests()
  {
    _dataStore = Substitute.For<ICalendarDataStore>();
    _sut = new CalendarService(_dataStore);
  }

  [Fact]
  public async Task GetCalendarAsync_ShouldReturnMappedCalendar()
  {
    // Arrange
    var calendar = new CalendarEntity
    {
      Id = Guid.NewGuid(),
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };

    _dataStore.GetCalendarAsync(calendar.Id, TestContext.Current.CancellationToken).Returns(calendar);

    // Act
    var (status, result) = await _sut.GetCalendarAsync(calendar.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarResponseDto>();
    result.Id.ShouldBe(calendar.Id);
    result.Name.ShouldBe(calendar.Name);
  }

  [Fact]
  public async Task GetCalendarAsync_WhenCalendarExists_ShouldReturnMappedCalendar()
  {
    // Arrange
    var calendarId = Guid.NewGuid();
    var calendar = new CalendarEntity
    {
      Id = calendarId,
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };
    _dataStore.GetCalendarAsync(calendarId, TestContext.Current.CancellationToken).Returns(calendar);

    // Act
    var (status, result) = await _sut.GetCalendarAsync(calendarId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarResponseDto>();
    result.Id.ShouldBe(calendar.Id);
    result.Name.ShouldBe(calendar.Name);
    result.Description.ShouldBe(calendar.Description);
  }

  [Fact]
  public async Task GetCalendarAsync_WhenCalendarDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var calendarId = Guid.NewGuid();
    _dataStore.GetCalendarAsync(calendarId, TestContext.Current.CancellationToken).Returns((CalendarEntity?)null);

    // Act
    var (status, result) = await _sut.GetCalendarAsync(calendarId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllCalendars_ShouldReturnListOfMappedCalendars()
  {
    // Arrange
    var calendars = new List<CalendarEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Calendar 1",
        Description = "Description",
        Color = "Red",
        Owner = "Me",
        Visibility = "Public"
      },
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Calendar 2",
        Description = "Description",
        Color = "Blue",
        Owner = "Me",
        Visibility = "Public"
      }
    };

    _dataStore.GetAllCalendarsAsync(TestContext.Current.CancellationToken).Returns(calendars.BuildMock());

    // Act
    var result = await _sut.GetAllCalendarsAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<CalendarResponseDto>();
    result[0].Id.ShouldBe(calendars[0].Id);
    result[1].Id.ShouldBe(calendars[1].Id);
  }

  [Fact]
  public async Task CreateCalendarAsync_ShouldCreateAndReturnMappedCalendar()
  {
    // Arrange
    var calendarRequestDto = new CalendarRequestDto
    {
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };
    var createdCalendar = new CalendarEntity
    {
      Id = Guid.CreateVersion7(),
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };
    _dataStore.CreateCalendarAsync(Arg.Any<CalendarEntity>(), TestContext.Current.CancellationToken).Returns(createdCalendar);

    // Act
    var result = await _sut.CreateCalendarAsync(calendarRequestDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarResponseDto>();
    result.Id.ShouldBe(createdCalendar.Id);
    result.Name.ShouldBe(calendarRequestDto.Name);
    result.Description.ShouldBe(calendarRequestDto.Description);
    await _dataStore
      .Received(1)
      .CreateCalendarAsync(
        Arg.Is<CalendarEntity>(c =>
          c.Name == calendarRequestDto.Name &&
          c.Description == calendarRequestDto.Description
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateCalendarAsync_WhenCalendarExists_ShouldUpdateAndReturnMappedCalendar()
  {
    // Arrange
    var calendarId = Guid.CreateVersion7();
    var existingCalendar = new CalendarEntity
    {
      Id = calendarId,
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };
    var calendarRequestDto = new CalendarRequestDto
    {
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };
    var updatedCalendar = new CalendarEntity
    {
      Id = calendarId,
      Name = calendarRequestDto.Name,
      Description = calendarRequestDto.Description,
      Color = calendarRequestDto.Color,
      Owner = calendarRequestDto.Owner,
      Visibility = calendarRequestDto.Visibility
    };
    _dataStore.GetCalendarAsync(calendarId, TestContext.Current.CancellationToken).Returns(existingCalendar);
    _dataStore.UpdateCalendarAsync(existingCalendar, Arg.Any<CalendarEntity>(), TestContext.Current.CancellationToken).Returns(updatedCalendar);

    // Act
    var (status, result) = await _sut.UpdateCalendarAsync(calendarRequestDto, calendarId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CalendarResponseDto>();
    result.Name.ShouldBe(calendarRequestDto.Name);
    result.Description.ShouldBe(calendarRequestDto.Description);
    await _dataStore.Received(1).UpdateCalendarAsync(existingCalendar, Arg.Any<CalendarEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateCalendarAsync_WhenCalendarDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var calendarId = Guid.CreateVersion7();
    var calendarRequestDto = new CalendarRequestDto
    {
      Name = "Calendar",
      Description = "Description",
      Color = "Red",
      Owner = "Me",
      Visibility = "Public"
    };
    _dataStore.GetCalendarAsync(calendarId, TestContext.Current.CancellationToken).Returns((CalendarEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateCalendarAsync(calendarRequestDto, calendarId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateCalendarAsync(Arg.Any<CalendarEntity>(), Arg.Any<CalendarEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteCalendar_ShouldCallDataStoreDelete()
  {
    // Arrange
    var calendarId = Guid.CreateVersion7();

    // Act
    await _sut.DeleteCalendarAsync(calendarId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteCalendarAsync(calendarId, TestContext.Current.CancellationToken);
  }
}
