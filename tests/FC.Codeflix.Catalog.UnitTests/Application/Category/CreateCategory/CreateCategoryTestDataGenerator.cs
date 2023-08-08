namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputList = new List<object[]>
        {
            new object[] { fixture.GetInvalidInputShortName(), "Name should be at least 3 characters long" },
            new object[] { fixture.GetInvalidInputTooLongName(), "Name should be less or equal 255 characters long"},
            new object[] { fixture.GetInvalidInputDescriptionNull(), "Description should not be null"},
            new object[] { fixture.GetInvalidInputTooLongDescription(), "Description should not be greater than 10000 characters long"}
        };
        
        return invalidInputList;
    }
}