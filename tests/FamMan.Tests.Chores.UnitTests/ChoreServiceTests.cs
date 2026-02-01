using System;
using FamMan.Api.Chores.Dtos;
using FamMan.Api.Chores.Entities;
using FamMan.Api.Chores.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace FamMan.Tests.Chores.UnitTests;

public class ChoreServiceTests
{
  private readonly IChoreDataStore _dataStore;
  private readonly ChoreService _sut;

  public ChoreServiceTests()
  {
    _dataStore = Substitute.For<IChoreDataStore>();
    _sut = new ChoreService(_dataStore);
  }

  [Fact]
  public async Task GetChoresAsync_ShouldReturnMappedChores()
  {
    // Arrange
    var chores = new List<Chore>
        {
            new() { Id = Guid.NewGuid(), Name = "Chore 1", Description = "Desc 1", CreatedAt = DateTime.UtcNow, DueDate = DateTime.UtcNow.AddDays(1) },
            new() { Id = Guid.NewGuid(), Name = "Chore 2", Description = "Desc 2", CreatedAt = DateTime.UtcNow, DueDate = DateTime.UtcNow.AddDays(2) }
        };
    _dataStore.GetChores().Returns(chores.BuildMock());

    // Act
    var result = await _sut.GetChoresAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.Count.ShouldBe(2);
    result[0].Id.ShouldBe(chores[0].Id);
    result[0].Name.ShouldBe(chores[0].Name);
    result[1].Id.ShouldBe(chores[1].Id);
    result[1].Name.ShouldBe(chores[1].Name);
  }

  [Fact]
  public async Task GetChoreAsync_WhenChoreExists_ShouldReturnMappedChore()
  {
    // Arrange
    var choreId = Guid.NewGuid();
    var chore = new Chore
    {
      Id = choreId,
      Name = "Test Chore",
      Description = "Test Description",
      CreatedAt = DateTime.UtcNow,
      DueDate = DateTime.UtcNow.AddDays(1)
    };
    _dataStore.GetChoreAsync(choreId, TestContext.Current.CancellationToken).Returns(chore);

    // Act
    var (status, result) = await _sut.GetChoreAsync(choreId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.Id.ShouldBe(chore.Id);
    result.Name.ShouldBe(chore.Name);
    result.Description.ShouldBe(chore.Description);
    result.CreatedAt.ShouldBe(chore.CreatedAt);
    result.DueDate.ShouldBe(chore.DueDate);
  }

  [Fact]
  public async Task GetChoreAsync_WhenChoreDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var choreId = Guid.NewGuid();
    _dataStore.GetChoreAsync(choreId, TestContext.Current.CancellationToken).Returns((Chore?)null);

    // Act
    var (status, result) = await _sut.GetChoreAsync(choreId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task CreateChoreAsync_ShouldCreateAndReturnMappedChore()
  {
    // Arrange
    var choreDto = new ChoreDto
    {
      Id = Guid.NewGuid(),
      Name = "New Chore",
      Description = "New Description",
      CreatedAt = DateTime.UtcNow,
      DueDate = DateTime.UtcNow.AddDays(1)
    };
    var createdChore = new Chore
    {
      Id = choreDto.Id,
      Name = choreDto.Name,
      Description = choreDto.Description,
      CreatedAt = choreDto.CreatedAt,
      DueDate = choreDto.DueDate
    };
    _dataStore.CreateChoreAsync(Arg.Any<Chore>(), TestContext.Current.CancellationToken).Returns(createdChore);

    // Act
    var result = await _sut.CreateChoreAsync(choreDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.Id.ShouldBe(choreDto.Id);
    result.Name.ShouldBe(choreDto.Name);
    result.Description.ShouldBe(choreDto.Description);
    await _dataStore.Received(1).CreateChoreAsync(Arg.Is<Chore>(c =>
        c.Name == choreDto.Name &&
        c.Description == choreDto.Description), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateChoreAsync_WhenChoreExists_ShouldUpdateAndReturnChore()
  {
    // Arrange
    var choreId = Guid.NewGuid();
    var existingChore = new Chore
    {
      Id = choreId,
      Name = "Old Name",
      Description = "Old Description",
      CreatedAt = DateTime.UtcNow.AddDays(-5),
      DueDate = DateTime.UtcNow
    };
    var choreDto = new ChoreDto
    {
      Id = choreId,
      Name = "Updated Name",
      Description = "Updated Description",
      CreatedAt = existingChore.CreatedAt,
      DueDate = DateTime.UtcNow.AddDays(1)
    };
    var updatedChore = new Chore
    {
      Id = choreId,
      Name = choreDto.Name,
      Description = choreDto.Description,
      CreatedAt = choreDto.CreatedAt,
      DueDate = choreDto.DueDate
    };
    _dataStore.GetChoreAsync(choreId, TestContext.Current.CancellationToken).Returns(existingChore);
    _dataStore.UpdateChoreAsync(existingChore, Arg.Any<Chore>(), TestContext.Current.CancellationToken).Returns(updatedChore);

    // Act
    var (status, chore) = await _sut.UpdateChoreAsync(choreDto, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    chore.ShouldNotBeNull();
    chore.Name.ShouldBe(choreDto.Name);
    chore.Description.ShouldBe(choreDto.Description);
    await _dataStore.Received(1).UpdateChoreAsync(existingChore, Arg.Any<Chore>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateChoreAsync_WhenChoreDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var choreId = Guid.NewGuid();
    var choreDto = new ChoreDto
    {
      Id = choreId,
      Name = "Updated Name",
      Description = "Updated Description"
    };
    _dataStore.GetChoreAsync(choreId, TestContext.Current.CancellationToken).Returns((Chore?)null);

    // Act
    var (status, chore) = await _sut.UpdateChoreAsync(choreDto, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    chore.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateChoreAsync(Arg.Any<Chore>(), Arg.Any<Chore>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteChore_ShouldCallDataStoreDelete()
  {
    // Arrange
    var choreId = Guid.NewGuid();

    // Act
    await _sut.DeleteChoreAsync(choreId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteChoreAsync(choreId, TestContext.Current.CancellationToken);
  }
}