namespace FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategory : IUpdateCategory
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategory(
        ICategoryRepository repository,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryModelOutput> Handle(
        UpdateCategoryInput request,
        CancellationToken cancellationToken
    )
    {
        var category = await _repository.Get(request.Id, cancellationToken);

        category.Update(request.Name, request.Description);

        if (request.IsActive is not null && request.IsActive != category.IsActive)
        {
            if (request.IsActive == true)
                category.Activate();
            else
                category.Deactivate();
        }

        await _repository.Update(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return CategoryModelOutput.FromCategory(category);
    }
}