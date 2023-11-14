using System.Diagnostics.CodeAnalysis;

namespace Service.Infrastructure.Exceptions;

public sealed class BreakException : ExceptionBase
{
    [DoesNotReturn]
    public static void Throw() =>
        throw new BreakException();

    [DoesNotReturn]
    public static TResult Throw<TResult>() =>
        throw new BreakException();
}