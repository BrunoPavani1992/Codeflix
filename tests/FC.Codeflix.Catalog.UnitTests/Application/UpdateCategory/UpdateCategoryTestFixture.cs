namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public UpdateCategoryInput GetValidInput(
        Guid? id = null
    ) =>
        new(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );
    
    public UpdateCategoryInput GetInvalidInputShortName()
    {
        var result = GetValidInput();
        result.Name = result.Name[..2];
        return result;
    }
    
    public UpdateCategoryInput GetInvalidInputTooLongName()
    {
        var result = GetValidInput();
        var tooLongName = Faker.Commerce.ProductName();
        while (tooLongName.Length <= 255)
            tooLongName = $"{tooLongName} {Faker.Commerce.ProductName()}";
        result.Name = tooLongName;
        return result;
    }

    public UpdateCategoryInput GetInvalidInputTooLongDescription()
    {
        var result = GetValidInput();
        var tooLongDescription = Faker.Commerce.ProductDescription();
        while (tooLongDescription.Length <= 10_000)
            tooLongDescription = $"{tooLongDescription} {Faker.Commerce.ProductDescription()}";
        result.Description = tooLongDescription;
        return result;
    }
}

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>
{
}