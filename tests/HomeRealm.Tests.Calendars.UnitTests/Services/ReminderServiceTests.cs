using HomeRealm.Api.Calendars.Dtos.Reminders;
using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.Reminders;
using HomeRealm.Api.Calendars.Services.Reminders;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace HomeRealm.Tests.Calendars.UnitTests.Services;

public class ReminderServiceTests
{
  private readonly IReminderDataStore _dataStore;
  private readonly ReminderService _sut;

  public ReminderServiceTests()
  {
    _dataStore = Substitute.For<IReminderDataStore>();
    _sut = new ReminderService(_dataStore);
  }

  [Fact]
  public async Task GetReminderAsync_WhenReminderExists_ShouldReturnMappedReminder()
  {
    // Arrange
    var reminder = new ReminderEntity
    {
      Id = Guid.NewGuid(),
      EventId = Guid.NewGuid(),
      Method = "Email",
      TimeBefore = 15
    };
    _dataStore.GetReminderAsync(reminder.Id, TestContext.Current.CancellationToken).Returns(reminder);

    // Act
    var (status, result) = await _sut.GetReminderAsync(reminder.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<ReminderResponseDto>();
    result.Id.ShouldBe(reminder.Id);
    result.Method.ShouldBe(reminder.Method);
  }

  [Fact]
  public async Task GetReminderAsync_WhenReminderDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var reminderId = Guid.NewGuid();
    _dataStore.GetReminderAsync(reminderId, TestContext.Current.CancellationToken).Returns((ReminderEntity?)null);

    // Act
    var (status, result) = await _sut.GetReminderAsync(reminderId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllRemindersAsync_ShouldReturnListOfMappedReminders()
  {
    // Arrange
    var reminders = new List<ReminderEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        EventId = Guid.NewGuid(),
        Method = "Email",
        TimeBefore = 15
      },
      new()
      {
        Id = Guid.NewGuid(),
        EventId = Guid.NewGuid(),
        Method = "SMS",
        TimeBefore = 30
      }
    };

    _dataStore.GetAllReminders().Returns(reminders.BuildMock());

    // Act
    var result = await _sut.GetAllRemindersAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<ReminderResponseDto>();
    result[0].Id.ShouldBe(reminders[0].Id);
    result[1].Id.ShouldBe(reminders[1].Id);
  }

  [Fact]
  public async Task CreateReminderAsync_ShouldCreateAndReturnMappedReminder()
  {
    // Arrange
    var eventId = Guid.NewGuid();
    var ReminderDto = new ReminderDto
    {
      EventId = eventId,
      Method = "Email",
      TimeBefore = 15
    };
    var createdReminder = new ReminderEntity
    {
      Id = Guid.CreateVersion7(),
      EventId = eventId,
      Method = "Email",
      TimeBefore = 15
    };
    _dataStore.CreateReminderAsync(Arg.Any<ReminderEntity>(), TestContext.Current.CancellationToken).Returns(createdReminder);

    // Act
    var result = await _sut.CreateReminderAsync(ReminderDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<ReminderResponseDto>();
    result.Id.ShouldBe(createdReminder.Id);
    result.Method.ShouldBe(ReminderDto.Method);
    await _dataStore
      .Received(1)
      .CreateReminderAsync(
        Arg.Is<ReminderEntity>(r =>
          r.EventId == ReminderDto.EventId &&
          r.Method == ReminderDto.Method
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateReminderAsync_WhenReminderExists_ShouldUpdateAndReturnMappedReminder()
  {
    // Arrange
    var reminderId = Guid.CreateVersion7();
    var eventId = Guid.NewGuid();
    var existingReminder = new ReminderEntity
    {
      Id = reminderId,
      EventId = eventId,
      Method = "Email",
      TimeBefore = 15
    };
    var ReminderDto = new ReminderDto
    {
      EventId = eventId,
      Method = "SMS",
      TimeBefore = 30
    };
    var updatedReminder = new ReminderEntity
    {
      Id = reminderId,
      EventId = ReminderDto.EventId,
      Method = ReminderDto.Method,
      TimeBefore = ReminderDto.TimeBefore
    };
    _dataStore.GetReminderAsync(reminderId, TestContext.Current.CancellationToken).Returns(existingReminder);
    _dataStore.UpdateReminderAsync(existingReminder, Arg.Any<ReminderEntity>(), TestContext.Current.CancellationToken).Returns(updatedReminder);

    // Act
    var (status, result) = await _sut.UpdateReminderAsync(ReminderDto, reminderId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<ReminderResponseDto>();
    result.Method.ShouldBe(ReminderDto.Method);
    await _dataStore.Received(1).GetReminderAsync(reminderId, TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateReminderAsync_WhenReminderDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var reminderId = Guid.NewGuid();
    var ReminderDto = new ReminderDto
    {
      EventId = Guid.NewGuid(),
      Method = "Email",
      TimeBefore = 15
    };
    _dataStore.GetReminderAsync(reminderId, TestContext.Current.CancellationToken).Returns((ReminderEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateReminderAsync(ReminderDto, reminderId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateReminderAsync(Arg.Any<ReminderEntity>(), Arg.Any<ReminderEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteReminderAsync_ShouldCallDataStore()
  {
    // Arrange
    var reminderId = Guid.NewGuid();

    // Act
    await _sut.DeleteReminderAsync(reminderId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteReminderAsync(reminderId, TestContext.Current.CancellationToken);
  }
}
