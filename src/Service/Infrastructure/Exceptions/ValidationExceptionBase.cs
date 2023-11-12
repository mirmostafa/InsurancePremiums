
namespace Infrastructure.Exceptions;

public abstract class ValidationExceptionBase : ExceptionBase
{
    protected ValidationExceptionBase()
    {
    }

    protected ValidationExceptionBase(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}