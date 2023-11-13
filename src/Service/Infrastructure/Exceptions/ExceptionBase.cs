namespace Service.Infrastructure.Exceptions;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase()
    {
    }

    protected ExceptionBase(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}