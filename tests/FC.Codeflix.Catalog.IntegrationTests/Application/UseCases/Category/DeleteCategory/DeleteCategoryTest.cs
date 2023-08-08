using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(
        DeleteCategoryTestFixture fixture
    )
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("Integration/Application", "DeleteCategory - Use Cases")]
    public async Task DeleteCategory()
    {
        var exampleList = _fixture.GetValidCategoriesList();
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var exampleCategory = _fixture.GetValidCategory();
        await dbContext.AddRangeAsync(exampleList);
        var tracking = await dbContext.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync();
        tracking.State = EntityState.Detached;
        var input = new DeleteCategoryInput(exampleCategory.Id);
        var useCase = new UseCase.DeleteCategory(repository, unitOfWork);

        await useCase.Handle(input, CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var dbCategoryDeleted = await assertDbContext.Categories.FindAsync(exampleCategory.Id);
        dbCategoryDeleted.Should().BeNull();
        var dbCategories = await assertDbContext.Categories.ToListAsync();
        dbCategories.Should().HaveCount(exampleList.Count);
    }
}