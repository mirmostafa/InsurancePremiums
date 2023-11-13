namespace Service.Infrastructure.Exceptions;

public sealed class NullValueValidationException : ValidationExceptionBase
{
    public NullValueValidationException(string argName)
    {
        this.ArgName = argName;
    }
    public string? ArgName { get; }

    public NullValueValidationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}