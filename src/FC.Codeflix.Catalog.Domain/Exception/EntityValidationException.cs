namespace FC.Codeflix.Catalog.Domain.Exception;

public class EntityValidationException : System.Exception
{
    public EntityValidationException(string? message) : base(message)
    {
    }
}