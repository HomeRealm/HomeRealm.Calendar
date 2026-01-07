using FamMan.Api.Calendars.Dtos.Category;
using FamMan.Api.Calendars.Entities;
using FamMan.Api.Calendars.Interfaces.Category;
using FamMan.Api.Calendars.Services.Category;
using MockQueryable;
using NSubstitute;
using Shouldly;

namespace FamMan.Tests.Calendars.UnitTests.Services;

public class CategoryServiceTests
{
  private readonly ICategoryDataStore _dataStore;
  private readonly CategoryService _sut;

  public CategoryServiceTests()
  {
    _dataStore = Substitute.For<ICategoryDataStore>();
    _sut = new CategoryService(_dataStore);
  }

  [Fact]
  public async Task GetCategoryAsync_WhenCategoryExists_ShouldReturnMappedCategory()
  {
    // Arrange
    var category = new CategoryEntity
    {
      Id = Guid.NewGuid(),
      Name = "Work",
      Color = "Blue",
      Icon = "briefcase"
    };
    _dataStore.GetCategoryAsync(category.Id, TestContext.Current.CancellationToken).Returns(category);

    // Act
    var (status, result) = await _sut.GetCategoryAsync(category.Id, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("found");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CategoryResponseDto>();
    result.Id.ShouldBe(category.Id);
    result.Name.ShouldBe(category.Name);
  }

  [Fact]
  public async Task GetCategoryAsync_WhenCategoryDoesNotExist_ShouldReturnNull()
  {
    // Arrange
    var categoryId = Guid.NewGuid();
    _dataStore.GetCategoryAsync(categoryId, TestContext.Current.CancellationToken).Returns((CategoryEntity?)null);

    // Act
    var (status, result) = await _sut.GetCategoryAsync(categoryId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
  }

  [Fact]
  public async Task GetAllCategoriesAsync_ShouldReturnListOfMappedCategories()
  {
    // Arrange
    var categories = new List<CategoryEntity>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Work",
        Color = "Blue",
        Icon = "briefcase"
      },
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Personal",
        Color = "Green",
        Icon = "person"
      }
    };

    _dataStore.GetAllCategoriesAsync(TestContext.Current.CancellationToken).Returns(categories.BuildMock());

    // Act
    var result = await _sut.GetAllCategoriesAsync(TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result[0].ShouldBeOfType<CategoryResponseDto>();
    result[0].Id.ShouldBe(categories[0].Id);
    result[1].Id.ShouldBe(categories[1].Id);
  }

  [Fact]
  public async Task CreateCategoryAsync_ShouldCreateAndReturnMappedCategory()
  {
    // Arrange
    var categoryRequestDto = new CategoryRequestDto
    {
      Name = "Work",
      Color = "Blue",
      Icon = "briefcase"
    };
    var createdCategory = new CategoryEntity
    {
      Id = Guid.CreateVersion7(),
      Name = "Work",
      Color = "Blue",
      Icon = "briefcase"
    };
    _dataStore.CreateCategoryAsync(Arg.Any<CategoryEntity>(), TestContext.Current.CancellationToken).Returns(createdCategory);

    // Act
    var result = await _sut.CreateCategoryAsync(categoryRequestDto, TestContext.Current.CancellationToken);

    // Assert
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CategoryResponseDto>();
    result.Id.ShouldBe(createdCategory.Id);
    result.Name.ShouldBe(categoryRequestDto.Name);
    await _dataStore
      .Received(1)
      .CreateCategoryAsync(
        Arg.Is<CategoryEntity>(c =>
          c.Name == categoryRequestDto.Name &&
          c.Color == categoryRequestDto.Color
        ),
        TestContext.Current.CancellationToken
      );
  }

  [Fact]
  public async Task UpdateCategoryAsync_WhenCategoryExists_ShouldUpdateAndReturnMappedCategory()
  {
    // Arrange
    var categoryId = Guid.CreateVersion7();
    var existingCategory = new CategoryEntity
    {
      Id = categoryId,
      Name = "Work",
      Color = "Blue",
      Icon = "briefcase"
    };
    var categoryRequestDto = new CategoryRequestDto
    {
      Name = "Work Updated",
      Color = "Red",
      Icon = "work"
    };
    var updatedCategory = new CategoryEntity
    {
      Id = categoryId,
      Name = categoryRequestDto.Name,
      Color = categoryRequestDto.Color,
      Icon = categoryRequestDto.Icon
    };
    _dataStore.GetCategoryAsync(categoryId, TestContext.Current.CancellationToken).Returns(existingCategory);
    _dataStore.UpdateCategoryAsync(existingCategory, Arg.Any<CategoryEntity>(), TestContext.Current.CancellationToken).Returns(updatedCategory);

    // Act
    var (status, result) = await _sut.UpdateCategoryAsync(categoryRequestDto, categoryId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("updated");
    result.ShouldNotBeNull();
    result.ShouldBeOfType<CategoryResponseDto>();
    result.Name.ShouldBe(categoryRequestDto.Name);
    await _dataStore.Received(1).GetCategoryAsync(categoryId, TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task UpdateCategoryAsync_WhenCategoryDoesNotExist_ShouldReturnNotFound()
  {
    // Arrange
    var categoryId = Guid.NewGuid();
    var categoryRequestDto = new CategoryRequestDto
    {
      Name = "Work",
      Color = "Blue",
      Icon = "briefcase"
    };
    _dataStore.GetCategoryAsync(categoryId, TestContext.Current.CancellationToken).Returns((CategoryEntity?)null);

    // Act
    var (status, result) = await _sut.UpdateCategoryAsync(categoryRequestDto, categoryId, TestContext.Current.CancellationToken);

    // Assert
    status.ShouldBe("notfound");
    result.ShouldBeNull();
    await _dataStore.DidNotReceive().UpdateCategoryAsync(Arg.Any<CategoryEntity>(), Arg.Any<CategoryEntity>(), TestContext.Current.CancellationToken);
  }

  [Fact]
  public async Task DeleteCategoryAsync_ShouldCallDataStore()
  {
    // Arrange
    var categoryId = Guid.NewGuid();

    // Act
    await _sut.DeleteCategoryAsync(categoryId, TestContext.Current.CancellationToken);

    // Assert
    await _dataStore.Received(1).DeleteCategoryAsync(categoryId, TestContext.Current.CancellationToken);
  }
}
