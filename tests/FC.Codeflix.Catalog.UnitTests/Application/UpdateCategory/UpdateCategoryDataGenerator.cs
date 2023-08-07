using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

public class UpdateCategoryDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(
        int times = 10
    )
    {
        var fixture = new UpdateCategoryTestFixture();
        for (var i = 0; i < times; i++)
        {
            var exampleCategory = fixture.GetValidCategory();
            var exampleInput = fixture.GetValidInput(exampleCategory.Id);
            yield return new object[]
            {
                exampleCategory, exampleInput
            };
        }
    }
    
    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new UpdateCategoryTestFixture();
        var invalidInputList = new List<object[]>
        {
            new object[] { fixture.GetInvalidInputShortName(), "Name should not be less than 3 characters long" },
            new object[] { fixture.GetInvalidInputTooLongName(), "Name should not be greater than 255 characters long"},
            new object[] { fixture.GetInvalidInputTooLongDescription(), "Description should not be greater than 10000 characters long"}
        };
        
        return invalidInputList;
    }
}