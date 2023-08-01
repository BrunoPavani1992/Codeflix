namespace FC.Codeflix.Catalog.UnitTests.Domain.Validation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validators")]
    public void NotNullOk()
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var value = Faker.Commerce.ProductName();

        var action = () => DomainValidation.NotNull(value, fieldName);

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "DomainValidation - Validators")]
    public void NotNullThrowWhenNull()
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        string? value = null;

        var action = () => DomainValidation.NotNull(value, fieldName);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage($"{fieldName} should not be null");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validators")]
    public void NotNullOrEmptyOk()
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var target = Faker.Commerce.ProductName();

        var action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - Validators")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var action = () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage($"{fieldName} should not be null or empty");
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validators")]
    [MemberData(nameof(GetValuesSmallerThanMin), parameters: 6)]
    public void MinLengthThrowWhenLess(string target, int minLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var action = () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage($"{fieldName} should not be less than {minLength} characters long");
    }
    
    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validators")]
    [MemberData(nameof(GetValuesGreaterThanMin), parameters: 6)]
    public void MinLengthOk(string target, int minLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var action = () => DomainValidation.MinLength(target, minLength, fieldName);

        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests)
    {
        yield return new object[] { "123456", 10 };

        var faker = new Faker();

        for (var i = 0; i < numberOfTests - 1; i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length + new Random().Next(1, 20);

            yield return new object[] { example, minLength };
        }
    }
    
    public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };

        var faker = new Faker();

        for (var i = 0; i < numberOfTests - 1; i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length - new Random().Next(1, 5);

            yield return new object[] { example, minLength };
        }
    }

    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreaterThanMax))]
    [Trait("Domain", "DomainValidation - Validators")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 6)]
    public void MaxLengthThrowWhenGreaterThanMax(string target, int maxLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should()
              .Throw<EntityValidationException>()
              .WithMessage($"{fieldName} should not be greater than {maxLength} characters long");
    }
    
    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "DomainValidation - Validators")]
    [MemberData(nameof(GetValuesLesserThanMax), parameters: 6)]
    public void MaxLengthOk(string target, int maxLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().NotThrow();
    }
    
    public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 5 };

        var faker = new Faker();

        for (var i = 0; i < numberOfTests - 1; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length - new Random().Next(1, 5);

            yield return new object[] { example, maxLength };
        }
    }
    
    public static IEnumerable<object[]> GetValuesLesserThanMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };

        var faker = new Faker();

        for (var i = 0; i < numberOfTests - 1; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length + new Random().Next(0, 5);

            yield return new object[] { example, maxLength };
        }
    }
}