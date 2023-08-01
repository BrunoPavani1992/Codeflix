namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory;

public class CreateCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
    
    public CreateCategoryTestFixture() : base()
    {
    }

    private string GetValidCategoryName()
    {
        var categoryName = "";

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    private string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];

        return categoryDescription;
    }

    private static bool GetRandomBoolean() => new Random().NextDouble() < 0.5;

    public CreateCategoryInput GetValidInput() =>
        new(GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );

    public CreateCategoryInput GetInvalidInputShortName()
    {
        var result = GetValidInput();
        result.Name = result.Name[..2];
        return result;
    }
    
    public CreateCategoryInput GetInvalidInputTooLongName()
    {
        var result = GetValidInput();
        var tooLongName = Faker.Commerce.ProductName();
        while (tooLongName.Length <= 255)
            tooLongName = $"{tooLongName} {Faker.Commerce.ProductName()}";
        result.Name = tooLongName;
        return result;
    }
    
    public CreateCategoryInput GetInvalidInputDescriptionNull()
    {
        var result = GetValidInput();
        result.Description = null;
        return result;
    }
    
    public CreateCategoryInput GetInvalidInputTooLongDescription()
    {
        var result = GetValidInput();
        var tooLongDescription = Faker.Commerce.ProductDescription();
        while (tooLongDescription.Length <= 10_000)
            tooLongDescription = $"{tooLongDescription} {Faker.Commerce.ProductDescription()}";
        result.Description = tooLongDescription;
        return result;
    }
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{
}