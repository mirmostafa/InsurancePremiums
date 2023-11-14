namespace Service.Infrastructure.Exceptions;

public sealed class NoItemFoundException : DetailedException
{
    public NoItemFoundException()
    {
    }

    public NoItemFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public NoItemFoundException(string message, string? instruction = null, string? title = null, string? details = null, Exception? inner = null, object? owner = null) : base(message, instruction, title, details, inner, owner)
    {
    }
}