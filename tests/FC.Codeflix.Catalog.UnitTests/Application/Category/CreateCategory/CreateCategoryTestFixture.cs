namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
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