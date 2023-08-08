using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.ListCategories;

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
        var dbContext = _fixture.CreateDbContext();
        var categoriesList = _fixture.GetExampleCategoriesList(10);
        var repository = new CategoryRepository(dbContext);
        await dbContext.AddRangeAsync(categoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var input = new ListCategoriesInput(1, 20);
        var useCase = new UseCase.ListCategories(repository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(categoriesList.Count);
        output.Items.Should().HaveCount(categoriesList.Count);

        foreach (var outputItem in output.Items)
        {
            var exampleItem = categoriesList.Find(category => category.Id == outputItem.Id);
            exampleItem.Should().NotBeNull();

            outputItem.Id.Should().Be(exampleItem!.Id);
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
            outputItem.IsActive.Should().Be(exampleItem.IsActive);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);   
        }
    }
}