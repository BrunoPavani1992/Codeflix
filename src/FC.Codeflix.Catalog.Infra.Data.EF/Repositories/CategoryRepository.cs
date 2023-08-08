namespace FC.Codeflix.Catalog.Infra.Data.EF.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CodeflixCatalogDbContext _context;
    private DbSet<Category> Categories => _context.Set<Category>();

    public CategoryRepository(
        CodeflixCatalogDbContext context
    )
    {
        _context = context;
    }

    public async Task Insert(
        Category aggregate,
        CancellationToken cancellationToken
    )
    {
        await Categories.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Category> Get(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var category = await Categories
                             .AsNoTracking()
                             .FirstOrDefaultAsync(
                                 x => x.Id == id,
                                 cancellationToken
                             );

        if (category == null)
            throw new NotFoundException($"Category '{id}' not found.");

        return category;
    }

    public Task Delete(
        Category aggregate,
        CancellationToken _
    )
    {
        return Task.FromResult(Categories.Remove(aggregate));
    }

    public Task Update(
        Category aggregate,
        CancellationToken _
    )
    {
        return Task.FromResult(Categories.Update(aggregate));
    }

    public async Task<SearchOutput<Category>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    )
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = Categories.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!string.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));

        var total = await query.CountAsync(cancellationToken);
        var items = await query
                          .AsNoTracking()
                          .Skip(toSkip)
                          .Take(input.PerPage)
                          .ToListAsync(cancellationToken);

        return new SearchOutput<Category>(input.Page, input.PerPage, total, items);
    }

    private IQueryable<Category> AddOrderToQuery(
        IQueryable<Category> query,
        string orderBy,
        SearchOrder order
    ) =>
        (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
        };
}