using HomeRealm.Api.Calendars.Dtos.OccurrenceOverrides;
using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.OccurrenceOverrides;
using HomeRealm.Api.Calendars.Services.OccurrenceOverrides;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace HomeRealm.Tests.Calendars.UnitTests.Services;

public class OccurrenceOverrideServiceTests
{
  private readonly IOccurrenceOverrideDataStore _dataStore;
  private readonly OccurrenceOverrideService _sut;

  public OccurrenceOverrideServiceTests()
  {
    _dataStore = Substitute.For<IOccurrenceOverrideDataStore>();
    _sut = new OccurrenceOverrideService(_dataStore);
  }

  [Fact]
  public async Task GetOccurrenceOverrideAsync_WhenOccurrenceOverrideExists_ShouldReturnMappedOccurrenceOverride()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var occurrenceOverride = new OccurrenceOverrideEntity
    {
      Id = Guid.NewGuid(),
      RecurrenceId = Guid.NewGuid(),
      Date = now
    };
    _dataStore.GetOccurrenceOverrideAsync(occurrenceOverride.Id, TestContext.Current.CancellationToken).Returns(occurrenceOverride);

    // Act
    var (status, result) = await _sut.GetOccurrenceOverrideAsync(occurrenceOverride.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<OccurrenceOverrideResponseDto>();
    result.Id.ShouldBe(occurrenceOverride.Id);
    result.Date.ShouldBe(occurrenceOverride.Date);
  }

  [Fact]
  public async Task GetOccurrenceOverrideAsync_WhenOccurrenceOverrideDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var occurrenceOverrideId = Guid.NewGuid();
    _dataStore.GetOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken).Returns((OccurrenceOverrideEntity?)null);

    // Act
    var (status, result) = await _sut.GetOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllOccurrenceOverridesAsync_ShouldReturnListOfMappedOccurrenceOverrides()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var occurrenceOverrides = new List<OccurrenceOverrideEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        RecurrenceId = Guid.NewGuid(),
        Date = now
      },
      new()
      {
        Id = Guid.NewGuid(),
        RecurrenceId = Guid.NewGuid(),
        Date = now.AddDays(1)
      }
    };

    _dataStore.GetAllOccurrenceOverrides().Returns(occurrenceOverrides.BuildMock());

    // Act
    var result = await _sut.GetAllOccurrenceOverridesAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<OccurrenceOverrideResponseDto>();
    result[0].Id.ShouldBe(occurrenceOverrides[0].Id);
    result[1].Id.ShouldBe(occurrenceOverrides[1].Id);
  }

  [Fact]
  public async Task CreateOccurrenceOverrideAsync_ShouldCreateAndReturnMappedOccurrenceOverride()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var recurrenceId = Guid.NewGuid();
    var OccurrenceOverrideDto = new OccurrenceOverrideDto
    {
      RecurrenceId = recurrenceId,
      Date = now
    };
    var createdOccurrenceOverride = new OccurrenceOverrideEntity
    {
      Id = Guid.CreateVersion7(),
      RecurrenceId = recurrenceId,
      Date = now
    };
    _dataStore.CreateOccurrenceOverrideAsync(Arg.Any<OccurrenceOverrideEntity>(), TestContext.Current.CancellationToken).Returns(createdOccurrenceOverride);

    // Act
    var result = await _sut.CreateOccurrenceOverrideAsync(OccurrenceOverrideDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<OccurrenceOverrideResponseDto>();
    result.Id.ShouldBe(createdOccurrenceOverride.Id);
    result.Date.ShouldBe(OccurrenceOverrideDto.Date);
    await _dataStore
      .Received(1)
      .CreateOccurrenceOverrideAsync(
        Arg.Is<OccurrenceOverrideEntity>(oo =>
          oo.RecurrenceId == OccurrenceOverrideDto.RecurrenceId &&
          oo.Date == OccurrenceOverrideDto.Date
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateOccurrenceOverrideAsync_WhenOccurrenceOverrideExists_ShouldUpdateAndReturnMappedOccurrenceOverride()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var occurrenceOverrideId = Guid.CreateVersion7();
    var recurrenceId = Guid.NewGuid();
    var existingOccurrenceOverride = new OccurrenceOverrideEntity
    {
      Id = occurrenceOverrideId,
      RecurrenceId = recurrenceId,
      Date = now
    };
    var OccurrenceOverrideDto = new OccurrenceOverrideDto
    {
      RecurrenceId = recurrenceId,
      Date = now.AddDays(1)
    };
    var updatedOccurrenceOverride = new OccurrenceOverrideEntity
    {
      Id = occurrenceOverrideId,
      RecurrenceId = OccurrenceOverrideDto.RecurrenceId,
      Date = OccurrenceOverrideDto.Date
    };
    _dataStore.GetOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken).Returns(existingOccurrenceOverride);
    _dataStore.UpdateOccurrenceOverrideAsync(existingOccurrenceOverride, Arg.Any<OccurrenceOverrideEntity>(), TestContext.Current.CancellationToken).Returns(updatedOccurrenceOverride);

    // Act
    var (status, result) = await _sut.UpdateOccurrenceOverrideAsync(OccurrenceOverrideDto, occurrenceOverrideId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<OccurrenceOverrideResponseDto>();
    result.Date.ShouldBe(OccurrenceOverrideDto.Date);
    await _dataStore.Received(1).GetOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateOccurrenceOverrideAsync_WhenOccurrenceOverrideDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var occurrenceOverrideId = Guid.NewGuid();
    var now = new DateTime(2026, 1, 7);
    var OccurrenceOverrideDto = new OccurrenceOverrideDto
    {
      RecurrenceId = Guid.NewGuid(),
      Date = now
    };
    _dataStore.GetOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken).Returns((OccurrenceOverrideEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateOccurrenceOverrideAsync(OccurrenceOverrideDto, occurrenceOverrideId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateOccurrenceOverrideAsync(Arg.Any<OccurrenceOverrideEntity>(), Arg.Any<OccurrenceOverrideEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteOccurrenceOverrideAsync_ShouldCallDataStore()
  {
    // Arrange
    var occurrenceOverrideId = Guid.NewGuid();

    // Act
    await _sut.DeleteOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteOccurrenceOverrideAsync(occurrenceOverrideId, TestContext.Current.CancellationToken);
  }
}
