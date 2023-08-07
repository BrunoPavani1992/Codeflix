namespace FC.Codeflix.Catalog.Domain.Repository;

public interface ICategoryRepository : IGenericRepository<Category>,
                                       ISearchableRepository<Category>
{
}