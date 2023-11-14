using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Service.Infrastructure.Bcl.Helpers;

public static class StringHelper
{
    /// <summary>
    /// Compacts an array of strings by removing any empty strings.
    /// </summary>
    /// <param name="strings">The strings to compact.</param>
    /// <returns>The compacted array of strings.</returns>
    [Pure]
    [return: NotNull]
    public static string[] Compact(params string[] strings) =>
        strings.Where(item => !item.IsNullOrEmpty()).ToArray();

    /// <summary>
    /// Filters out null or empty strings from the given IEnumerable and returns a new IEnumerable
    /// with only non-null and non-empty strings.
    /// </summary>
    /// <param name="strings">The IEnumerable of strings to filter.</param>
    /// <returns>A new IEnumerable with only non-null and non-empty strings.</returns>
    [Pure]
    [return: NotNull]
    public static IEnumerable<string> Compact(this IEnumerable<string?>? strings) =>
        (strings?.Where([StackTraceHidden][DebuggerStepThrough] (item) => !item.IsNullOrEmpty()).Select(s => s!)) ?? Enumerable.Empty<string>();

    /// <summary>
    /// Checks if the given string is null or empty.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) =>
        str is null or { Length: 0 };
}