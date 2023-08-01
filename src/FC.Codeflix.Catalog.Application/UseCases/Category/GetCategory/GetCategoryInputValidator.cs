namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
{
    public GetCategoryInputValidator()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");
        
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}