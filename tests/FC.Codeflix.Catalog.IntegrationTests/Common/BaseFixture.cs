namespace FC.Codeflix.Catalog.IntegrationTests.Common;

public class BaseFixture
{
    protected Faker Faker { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
    }
    
    public CodeflixCatalogDbContext CreateDbContext(
        bool preserveData = false
    )
    {
        var context = new CodeflixCatalogDbContext(
            new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );

        if (preserveData == false)
            context.Database.EnsureDeleted();

        return context;
    }
}