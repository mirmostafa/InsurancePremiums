namespace Service.Infrastructure.Exceptions;

public sealed class ValidationException : ValidationExceptionBase
{
    public ValidationException()
    {

    }
    public ValidationException(string prompt)
    {
        this.Prompt = prompt;
    }
    public string? Prompt { get; }
}