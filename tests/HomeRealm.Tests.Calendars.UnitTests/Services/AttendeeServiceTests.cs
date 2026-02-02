using HomeRealm.Api.Calendars.Dtos.Attendees;
using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.Attendees;
using HomeRealm.Api.Calendars.Services.Attendees;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace HomeRealm.Tests.Calendars.UnitTests.Services;

public class AttendeeServiceTests
{
  private readonly IAttendeeDataStore _dataStore;
  private readonly AttendeeService _sut;

  public AttendeeServiceTests()
  {
    _dataStore = Substitute.For<IAttendeeDataStore>();
    _sut = new AttendeeService(_dataStore);
  }

  [Fact]
  public async Task GetAttendeeAsync_WhenAttendeeExists_ShouldReturnMappedAttendee()
  {
    // Arrange
    var attendee = new AttendeeEntity
    {
      Id = Guid.NewGuid(),
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = "Confirmed",
      Role = "Guest"
    };
    _dataStore.GetAttendeeAsync(attendee.Id, TestContext.Current.CancellationToken).Returns(attendee);

    // Act
    var (status, result) = await _sut.GetAttendeeAsync(attendee.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<AttendeeResponseDto>();
    result.Id.ShouldBe(attendee.Id);
    result.Status.ShouldBe(attendee.Status);
  }

  [Fact]
  public async Task GetAttendeeAsync_WhenAttendeeDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var attendeeId = Guid.NewGuid();
    _dataStore.GetAttendeeAsync(attendeeId, TestContext.Current.CancellationToken).Returns((AttendeeEntity?)null);

    // Act
    var (status, result) = await _sut.GetAttendeeAsync(attendeeId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllAttendeesAsync_ShouldReturnListOfMappedAttendees()
  {
    // Arrange
    var attendees = new List<AttendeeEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        EventId = Guid.NewGuid(),
        UserId = Guid.NewGuid(),
        Status = "Confirmed",
        Role = "Guest"
      },
      new()
      {
        Id = Guid.NewGuid(),
        EventId = Guid.NewGuid(),
        UserId = Guid.NewGuid(),
        Status = "Pending",
        Role = "Host"
      }
    };

    _dataStore.GetAllAttendees().Returns(attendees.BuildMock());

    // Act
    var result = await _sut.GetAllAttendeesAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<AttendeeResponseDto>();
    result[0].Id.ShouldBe(attendees[0].Id);
    result[1].Id.ShouldBe(attendees[1].Id);
  }

  [Fact]
  public async Task CreateAttendeeAsync_ShouldCreateAndReturnMappedAttendee()
  {
    // Arrange
    var eventId = Guid.NewGuid();
    var userId = Guid.NewGuid();
    var AttendeeDto = new AttendeeDto
    {
      EventId = eventId,
      UserId = userId,
      Status = "Confirmed",
      Role = "Guest"
    };
    var createdAttendee = new AttendeeEntity
    {
      Id = Guid.CreateVersion7(),
      EventId = eventId,
      UserId = userId,
      Status = "Confirmed",
      Role = "Guest"
    };
    _dataStore.CreateAttendeeAsync(Arg.Any<AttendeeEntity>(), TestContext.Current.CancellationToken).Returns(createdAttendee);

    // Act
    var result = await _sut.CreateAttendeeAsync(AttendeeDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<AttendeeResponseDto>();
    result.Id.ShouldBe(createdAttendee.Id);
    result.Status.ShouldBe(AttendeeDto.Status);
    await _dataStore
      .Received(1)
      .CreateAttendeeAsync(
        Arg.Is<AttendeeEntity>(a =>
          a.EventId == AttendeeDto.EventId &&
          a.Status == AttendeeDto.Status
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateAttendeeAsync_WhenAttendeeExists_ShouldUpdateAndReturnMappedAttendee()
  {
    // Arrange
    var attendeeId = Guid.CreateVersion7();
    var eventId = Guid.NewGuid();
    var userId = Guid.NewGuid();
    var existingAttendee = new AttendeeEntity
    {
      Id = attendeeId,
      EventId = eventId,
      UserId = userId,
      Status = "Confirmed",
      Role = "Guest"
    };
    var AttendeeDto = new AttendeeDto
    {
      EventId = eventId,
      UserId = userId,
      Status = "Declined",
      Role = "Guest"
    };
    var updatedAttendee = new AttendeeEntity
    {
      Id = attendeeId,
      EventId = AttendeeDto.EventId,
      UserId = AttendeeDto.UserId,
      Status = AttendeeDto.Status,
      Role = AttendeeDto.Role
    };
    _dataStore.GetAttendeeAsync(attendeeId, TestContext.Current.CancellationToken).Returns(existingAttendee);
    _dataStore.UpdateAttendeeAsync(existingAttendee, Arg.Any<AttendeeEntity>(), TestContext.Current.CancellationToken).Returns(updatedAttendee);

    // Act
    var (status, result) = await _sut.UpdateAttendeeAsync(AttendeeDto, attendeeId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<AttendeeResponseDto>();
    result.Status.ShouldBe(AttendeeDto.Status);
    await _dataStore.Received(1).GetAttendeeAsync(attendeeId, TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateAttendeeAsync_WhenAttendeeDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var attendeeId = Guid.NewGuid();
    var AttendeeDto = new AttendeeDto
    {
      EventId = Guid.NewGuid(),
      UserId = Guid.NewGuid(),
      Status = "Confirmed",
      Role = "Guest"
    };
    _dataStore.GetAttendeeAsync(attendeeId, TestContext.Current.CancellationToken).Returns((AttendeeEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateAttendeeAsync(AttendeeDto, attendeeId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateAttendeeAsync(Arg.Any<AttendeeEntity>(), Arg.Any<AttendeeEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteAttendeeAsync_ShouldCallDataStore()
  {
    // Arrange
    var attendeeId = Guid.NewGuid();

    // Act
    await _sut.DeleteAttendeeAsync(attendeeId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteAttendeeAsync(attendeeId, TestContext.Current.CancellationToken);
  }
}
