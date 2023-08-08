using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(
        UpdateCategoryTestFixture fixture
    )
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(UpdateCategory))]
    [Trait("Integration/Application", "UpdateCategory - Use Cases")]
    [MemberData(nameof(UpdateCategoryDataGenerator.GetCategoriesToUpdate),
                parameters: 5,
                MemberType = typeof(UpdateCategoryDataGenerator)
    )]
    public async Task UpdateCategory(
        DomainEntity.Category exampleCategory,
        UpdateCategoryInput input
    )
    {
        var dbContext = _fixture.CreateDbContext();
        await dbContext.AddRangeAsync(_fixture.GetValidCategoriesList());
        var trackingInfo = await dbContext.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync();
        trackingInfo.State = EntityState.Detached;
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new UseCase.UpdateCategory(repository, unitOfWork);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive == true);
        
        var dbCategory = await dbContext.Categories
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == output.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(output.Name);
        dbCategory.Description.Should().Be(output.Description);
        dbCategory.IsActive.Should().Be(output.IsActive);
        dbCategory.CreatedAt.Should().Be(output.CreatedAt);
    }
}