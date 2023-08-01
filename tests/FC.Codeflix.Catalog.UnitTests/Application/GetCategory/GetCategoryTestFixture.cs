namespace FC.Codeflix.Catalog.UnitTests.Application.GetCategory;

public class GetCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    
    public string GetValidCategoryName()
    {
        var categoryName = "";

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];

        return categoryDescription;
    }
    
    public DomainEntity.Category GetValidCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription()
        );
}

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture>
{
}