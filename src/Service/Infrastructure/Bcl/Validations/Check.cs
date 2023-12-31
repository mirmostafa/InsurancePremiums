﻿using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Service.Infrastructure.Bcl.Helpers;
using Service.Infrastructure.Bcl.Results;
using Service.Infrastructure.Bcl.Validations;
using Service.Infrastructure.Exceptions;

using ValidationException = Service.Infrastructure.Exceptions.ValidationException;

namespace Service.Infrastructure.Bcl.Validations;

[DebuggerStepThrough]
[StackTraceHidden]
public sealed class Check
{
    private static Checker? _that;

    private Check()
    { }

    /// <summary> Gets the singleton instance of the <see cref="Checker"/> functionality. </summary>
    /// <remarks> Users can use this to plug-in custom assertions through C# extension methods. For
    /// instance, the signature of a custom assertion provider could be "public static void
    /// IsOfType<T>(this Assert assert, object obj)" Users could then use a syntax similar to the
    /// default assertions which in this case is "Assert.That.IsOfType<Dog>(animal);" </remarks>
    public static Checker That => _that ??= new();

    public static Result If(in bool notOk, in Func<string> getErrorMessage) =>
        notOk ? Result.CreateFailure(message: getErrorMessage()) : Result.CreateSuccess();

    public static Result If(in bool notOk) =>
        notOk ? Result.Failure : Result.Success;

    public static Result If(in bool notOk, in Func<Exception> getErrorMessage) =>
        notOk ? Result.CreateFailure(getErrorMessage()) : Result.CreateSuccess();

    public static Result<TValue> If<TValue>(in TValue value, in bool notOk, in Func<Exception> getErrorMessage) =>
        notOk ? Result<TValue>.CreateFailure(getErrorMessage(), value) : Result<TValue>.CreateSuccess(value);

    public static Result<IEnumerable<string?>?> IfAnyNull(in IEnumerable<string?>? items)
    {
        if (items?.Any() ?? false)
        {
            foreach (var item in items)
            {
                if (IfIsNull(item).TryParse(out var vr))
                {
                    return vr.WithValue(items)!;
                }
            }
        }

        return Result<IEnumerable<string?>?>.CreateSuccess(items);
    }

    public static Result<TValue> IfArgumentIsNull<TValue>(in TValue obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        If(obj, obj is null, () => new NullValueValidationException(argName!));

    public static Result<TValue> IfIsNull<TValue>(in TValue obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        If(obj, obj is null, () => new NullValueValidationException(argName!));

    /// <summary>
    /// Throws an exception if the specified boolean is false.
    /// </summary>
    /// <param name="ok">The boolean to check.</param>
    /// <param name="getExceptionIfNot">
    /// A function to get the exception to throw if the boolean is false.
    /// </param>
    public static void MustBe([DoesNotReturnIf(false)] bool ok, in Func<Exception> getExceptionIfNot)
    {
        if (!ok)
        {
            throw getExceptionIfNot();
        }
    }

    public static void MustBe([DoesNotReturnIf(false)] bool ok, Func<string> getMessageIfNot) =>
        MustBe(ok, () => new ValidationException(getMessageIfNot()));

    /// <summary>
    /// Checks if the given boolean is true, and throws a new instance of the specified exception if
    /// it is false.
    /// </summary>
    /// <typeparam name="TValidationException">
    /// The type of exception to throw if the boolean is false.
    /// </typeparam>
    /// <param name="ok">The boolean to check.</param>
    public static void MustBe<TValidationException>([DoesNotReturnIf(false)] bool ok) where TValidationException : Exception, new() =>
        MustBe(ok, () => new TValidationException());

    /// <summary>
    /// This method throws a ValidationException if the required parameter is false.
    /// </summary>
    public static void MustBe([DoesNotReturnIf(false)] bool required) =>
        MustBe<ValidationException>(required);

    /// <summary>
    /// Checks if the given argument is not null and throws an ArgumentNullException if it is.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <param name="argName">The name of the argument.</param>
    public static void MustBeArgumentNotNull([NotNull][AllowNull] object? obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        MustBe(obj is not null, () => new ArgumentNullException(argName));

    /// <summary>
    /// Checks if the given argument is not null and throws an ArgumentNullException if it is.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <param name="argName">The name of the argument.</param>
    public static void MustBeArgumentNotNull([NotNull][AllowNull] in string? obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        MustBe(!obj.IsNullOrEmpty(), () => new ArgumentNullException(argName));

    public static void MustBeNotNull([NotNull][AllowNull] object? obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        MustBe(obj is not null, () => new NullValueValidationException(argName!));

    public static void MustBeNotNull([NotNull][AllowNull] object? obj, Func<string> getMessage) =>
        MustBe(obj is not null, () => new NullValueValidationException(getMessage(), null));

    /// <summary>
    /// Checks if the given object is not null and throws an exception if it is.
    /// </summary>
    public static void MustBeNotNull([NotNull][AllowNull] object? obj, [DisallowNull] Func<Exception> getException) =>
        MustBe(obj is not null, getException);

    /// <summary>
    /// Checks if the given IEnumerable object has any items and throws an exception if it does not.
    /// </summary>
    [return: NotNull]
    public static void MustHaveAny([NotNull][AllowNull] IEnumerable? obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        MustBe(obj?.Any() ?? false, () => new ValidationException(argName!));

    /// <summary>
    /// Checks if the given object is not null and throws an ArgumentNullException if it is.
    /// </summary>
    public static void MutBeNotNull([NotNull][AllowNull] object? obj, [CallerArgumentExpression(nameof(obj))] string? argName = null) =>
        MustBe(obj is not null, () => new NullValueValidationException(argName!));

    public static void ThrowIfDisposed<T>(T @this, [DoesNotReturnIf(true)] bool disposed) where T : IDisposable =>
        MustBe(!disposed, () => new ObjectDisposedException(@this?.GetType().Name));
}