namespace FC.Codeflix.Catalog.Application.Exception;

public class NotFoundException : ApplicationException
{
    public NotFoundException(
        string? message
    ) : base(message)
    {
    }
}