using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
    public List<DomainEntity.Category> GetExampleCategoriesList(
        int length = 10
    )
    {
        var list = new List<DomainEntity.Category>();
        for (var i = 0; i < length; i++)
            list.Add(GetValidCategory());
        return list;
    }

    public ListCategoriesInput GetExampleInput()
    {
        var random = new Random();

        return new ListCategoriesInput(
            page: random.Next(1, 10),
            perPage: random.Next(15, 100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0, 10) > 5 
                ? SearchOrder.Asc 
                : SearchOrder.Desc
        );
    }
}

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>
{
}