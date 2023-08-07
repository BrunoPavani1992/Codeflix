namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategories : IListCategories
{
    private readonly ICategoryRepository _repository;

    public ListCategories(
        ICategoryRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<ListCategoriesOutput> Handle(
        ListCategoriesInput request,
        CancellationToken cancellationToken
    )
    {
        var searchOutput = await _repository.Search(
            new SearchInput(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            ),
            cancellationToken
        );

        return new ListCategoriesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                        .Select(CategoryModelOutput.FromCategory)
                        .ToList()
        );
    }
}