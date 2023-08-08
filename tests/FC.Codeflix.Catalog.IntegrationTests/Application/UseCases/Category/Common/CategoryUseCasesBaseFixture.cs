namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

public abstract class CategoryUseCasesBaseFixture : BaseFixture
{
    protected string GetValidCategoryName()
    {
        var categoryName = "";

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    protected string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];

        return categoryDescription;
    }

    protected static bool GetRandomBoolean() => new Random().NextDouble() < 0.5;

    public DomainEntity.Category GetValidCategory() =>
        new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );

    public List<DomainEntity.Category> GetValidCategoriesList(
        int length = 10
    ) =>
        Enumerable.Range(1, length).Select(_ => GetValidCategory()).ToList();
}