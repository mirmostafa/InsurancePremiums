namespace Service.Infrastructure.Exceptions;

public sealed class CommonException : DetailedException
{
    public CommonException()
    {
    }

    public CommonException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public CommonException(string message, string? instruction = null, string? title = null, string? details = null, Exception? inner = null, object? owner = null) : base(message, instruction, title, details, inner, owner)
    {
    }
}