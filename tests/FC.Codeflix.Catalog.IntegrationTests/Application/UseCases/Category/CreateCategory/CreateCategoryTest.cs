using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(
        CreateCategoryTestFixture fixture
    )
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Integration/Application", "CreateCategory - Use Cases")]
    public async Task CreateCategory()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new UseCase.CreateCategory(repository, unitOfWork);
        var input = _fixture.GetValidInput();

        var output = await useCase.Handle(input, CancellationToken.None);
        
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);
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