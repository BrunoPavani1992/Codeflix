﻿using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest
{
    private readonly ListCategoriesTestFixture _fixture;

    public ListCategoriesTest(
        ListCategoriesTestFixture fixture
    ) =>
        _fixture = fixture;

    [Fact(DisplayName = nameof(ListCategories))]
    [Trait("Application", "ListCategories - Use Cases")]
    public async Task ListCategories()
    {
        var categoriesList = _fixture.GetExampleCategoriesList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();
        var outputRepositorySearch = new SearchOutput<DomainEntity.Category>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: categoriesList,
            total: new Random().Next(50, 200)
        );
        repositoryMock.Setup(
            x => x.Search(
                It.Is<SearchInput>(
                    searchInput =>
                        searchInput.Page == input.Page &&
                        searchInput.PerPage == input.PerPage &&
                        searchInput.Search == input.Search &&
                        searchInput.OrderBy == input.Sort &&
                        searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryCategory!.Name);
            outputItem.Description.Should().Be(repositoryCategory.Description);
            outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
            outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
        });
        repositoryMock.Verify(
            x => x.Search(
                It.Is<SearchInput>(
                    searchInput =>
                        searchInput.Page == input.Page &&
                        searchInput.PerPage == input.PerPage &&
                        searchInput.Search == input.Search &&
                        searchInput.OrderBy == input.Sort &&
                        searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), 
            Times.Once
        );
    }
}