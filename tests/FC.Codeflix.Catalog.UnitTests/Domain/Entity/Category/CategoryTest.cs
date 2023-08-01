namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _categoryTestFixture;

    public CategoryTest(CategoryTestFixture categoryTestFixture)
    {
        _categoryTestFixture = categoryTestFixture;
    }

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
        var datetimeAfter = DateTime.Now;

        category.Should().NotBeNull();
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);

        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);

        category.IsActive.Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(name!, validCategory.Description);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be null or empty");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsNull()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(validCategory.Name, null!);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should not be null");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action = () => new DomainEntity.Category(invalidName, validCategory.Description);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be less than 3 characters long");
    }

    public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();

        for (var i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;

            yield return new object[]
            {
                fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)]
            };
        }
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category(invalidName, validCategory.Description);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidDescription = string.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category(validCategory.Name, invalidDescription);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should not be greater than 10000 characters long");
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Activate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
        category.Activate();

        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Deactivate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregates")]
    public void Update()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var categoryWithNewValues = _categoryTestFixture.GetValidCategory();

        category.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

        category.Name.Should().Be(categoryWithNewValues.Name);
        category.Description.Should().Be(categoryWithNewValues.Description);
    }

    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateOnlyName()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var categoryWithNewValues = _categoryTestFixture.GetValidCategory();
        var currentDescription = category.Description;

        category.Update(categoryWithNewValues.Name);

        category.Name.Should().Be(categoryWithNewValues.Name);
        category.Description.Should().Be(currentDescription);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var category = _categoryTestFixture.GetValidCategory();

        var action = () => category.Update(name!);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be null or empty");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("ac")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var category = _categoryTestFixture.GetValidCategory();

        var action = () => category.Update(invalidName);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be less than 3 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var invalidName = _categoryTestFixture.Faker.Lorem.Letter(256);

        var action = () => category.Update(invalidName);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Name should not be greater than 255 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);

        var invalidDescription = _categoryTestFixture.Faker.Lorem.Letter(10001);

        var action = () => category.Update(validCategory.Name, invalidDescription);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage("Description should not be greater than 10000 characters long");
    }
}