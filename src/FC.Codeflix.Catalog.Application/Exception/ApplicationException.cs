namespace FC.Codeflix.Catalog.Application.Exception;

public abstract class ApplicationException : System.Exception
{
    protected ApplicationException(string? message) : base(message)
    {
    }
}