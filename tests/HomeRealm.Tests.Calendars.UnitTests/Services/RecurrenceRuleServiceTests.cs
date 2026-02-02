using HomeRealm.Api.Calendars.Dtos.RecurrenceRules;
using HomeRealm.Api.Calendars.Entities;
using HomeRealm.Api.Calendars.Interfaces.RecurrenceRules;
using HomeRealm.Api.Calendars.Services.RecurrenceRules;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace HomeRealm.Tests.Calendars.UnitTests.Services;

public class RecurrenceRuleServiceTests
{
  private readonly IRecurrenceRuleDataStore _dataStore;
  private readonly RecurrenceRuleService _sut;

  public RecurrenceRuleServiceTests()
  {
    _dataStore = Substitute.For<IRecurrenceRuleDataStore>();
    _sut = new RecurrenceRuleService(_dataStore);
  }

  [Fact]
  public async Task GetRecurrenceRuleAsync_WhenRecurrenceRuleExists_ShouldReturnMappedRecurrenceRule()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var recurrenceRule = new RecurrenceRuleEntity
    {
      Id = Guid.NewGuid(),
      EventId = Guid.NewGuid(),
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };
    _dataStore.GetRecurrenceRuleAsync(recurrenceRule.Id, TestContext.Current.CancellationToken).Returns(recurrenceRule);

    // Act
    var (status, result) = await _sut.GetRecurrenceRuleAsync(recurrenceRule.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<RecurrenceRuleResponseDto>();
    result.Id.ShouldBe(recurrenceRule.Id);
    result.Rule.ShouldBe(recurrenceRule.Rule);
  }

  [Fact]
  public async Task GetRecurrenceRuleAsync_WhenRecurrenceRuleDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var recurrenceRuleId = Guid.NewGuid();
    _dataStore.GetRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken).Returns((RecurrenceRuleEntity?)null);

    // Act
    var (status, result) = await _sut.GetRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllRecurrenceRulesAsync_ShouldReturnListOfMappedRecurrenceRules()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var recurrenceRules = new List<RecurrenceRuleEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        EventId = Guid.NewGuid(),
        Rule = "FREQ=DAILY",
        OccurrenceOverrides = new List<Guid>(),
        EndDate = now.AddDays(30)
      },
      new()
      {
        Id = Guid.NewGuid(),
        EventId = Guid.NewGuid(),
        Rule = "FREQ=WEEKLY",
        OccurrenceOverrides = new List<Guid>(),
        EndDate = now.AddDays(60)
      }
    };

    _dataStore.GetAllRecurrenceRules().Returns(recurrenceRules.BuildMock());

    // Act
    var result = await _sut.GetAllRecurrenceRulesAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<RecurrenceRuleResponseDto>();
    result[0].Id.ShouldBe(recurrenceRules[0].Id);
    result[1].Id.ShouldBe(recurrenceRules[1].Id);
  }

  [Fact]
  public async Task CreateRecurrenceRuleAsync_ShouldCreateAndReturnMappedRecurrenceRule()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var eventId = Guid.NewGuid();
    var RecurrenceRuleDto = new RecurrenceRuleDto
    {
      EventId = eventId,
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };
    var createdRecurrenceRule = new RecurrenceRuleEntity
    {
      Id = Guid.CreateVersion7(),
      EventId = eventId,
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };
    _dataStore.CreateRecurrenceRuleAsync(Arg.Any<RecurrenceRuleEntity>(), TestContext.Current.CancellationToken).Returns(createdRecurrenceRule);

    // Act
    var result = await _sut.CreateRecurrenceRuleAsync(RecurrenceRuleDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<RecurrenceRuleResponseDto>();
    result.Id.ShouldBe(createdRecurrenceRule.Id);
    result.Rule.ShouldBe(RecurrenceRuleDto.Rule);
    await _dataStore
      .Received(1)
      .CreateRecurrenceRuleAsync(
        Arg.Is<RecurrenceRuleEntity>(rr =>
          rr.EventId == RecurrenceRuleDto.EventId &&
          rr.Rule == RecurrenceRuleDto.Rule
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateRecurrenceRuleAsync_WhenRecurrenceRuleExists_ShouldUpdateAndReturnMappedRecurrenceRule()
  {
    // Arrange
    var now = new DateTime(2026, 1, 7);
    var recurrenceRuleId = Guid.CreateVersion7();
    var eventId = Guid.NewGuid();
    var existingRecurrenceRule = new RecurrenceRuleEntity
    {
      Id = recurrenceRuleId,
      EventId = eventId,
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };
    var RecurrenceRuleDto = new RecurrenceRuleDto
    {
      EventId = eventId,
      Rule = "FREQ=WEEKLY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(60)
    };
    var updatedRecurrenceRule = new RecurrenceRuleEntity
    {
      Id = recurrenceRuleId,
      EventId = RecurrenceRuleDto.EventId,
      Rule = RecurrenceRuleDto.Rule,
      OccurrenceOverrides = RecurrenceRuleDto.OccurrenceOverrides,
      EndDate = RecurrenceRuleDto.EndDate
    };
    _dataStore.GetRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken).Returns(existingRecurrenceRule);
    _dataStore.UpdateRecurrenceRuleAsync(existingRecurrenceRule, Arg.Any<RecurrenceRuleEntity>(), TestContext.Current.CancellationToken).Returns(updatedRecurrenceRule);

    // Act
    var (status, result) = await _sut.UpdateRecurrenceRuleAsync(RecurrenceRuleDto, recurrenceRuleId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<RecurrenceRuleResponseDto>();
    result.Rule.ShouldBe(RecurrenceRuleDto.Rule);
    await _dataStore.Received(1).GetRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateRecurrenceRuleAsync_WhenRecurrenceRuleDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var recurrenceRuleId = Guid.NewGuid();
    var now = new DateTime(2026, 1, 7);
    var RecurrenceRuleDto = new RecurrenceRuleDto
    {
      EventId = Guid.NewGuid(),
      Rule = "FREQ=DAILY",
      OccurrenceOverrides = new List<Guid>(),
      EndDate = now.AddDays(30)
    };
    _dataStore.GetRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken).Returns((RecurrenceRuleEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateRecurrenceRuleAsync(RecurrenceRuleDto, recurrenceRuleId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateRecurrenceRuleAsync(Arg.Any<RecurrenceRuleEntity>(), Arg.Any<RecurrenceRuleEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteRecurrenceRuleAsync_ShouldCallDataStore()
  {
    // Arrange
    var recurrenceRuleId = Guid.NewGuid();

    // Act
    await _sut.DeleteRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteRecurrenceRuleAsync(recurrenceRuleId, TestContext.Current.CancellationToken);
  }
}
