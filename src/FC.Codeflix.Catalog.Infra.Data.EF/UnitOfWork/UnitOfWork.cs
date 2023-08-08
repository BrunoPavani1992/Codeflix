namespace FC.Codeflix.Catalog.Infra.Data.EF.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly CodeflixCatalogDbContext _context;

    public UnitOfWork(
        CodeflixCatalogDbContext context
    )
    {
        _context = context;
    }

    public async Task Commit(
        CancellationToken cancellationToken
    )
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public Task Rollback(
        CancellationToken cancellationToken
    )
    {
        return Task.CompletedTask;
    }
}