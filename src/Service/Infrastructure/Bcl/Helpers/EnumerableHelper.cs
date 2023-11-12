using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Bcl.Helpers;

public static class EnumerableHelper
{
    /// <summary>
    /// Adds a range of items to an existing IEnumerable in an immutable fashion.
    /// </summary>
    /// <typeparam name="T">The type of the items in the IEnumerable.</typeparam>
    /// <param name="source">The source IEnumerable.</param>
    /// <param name="items">The items to add.</param>
    /// <returns>A new IEnumerable containing the items from the source and the items to add.</returns>
    public static IEnumerable<T> AddRangeImmuted<T>(this IEnumerable<T>? source, IEnumerable<T>? items)
    {
        return (source, items) switch
        {
            (null, null) => Enumerable.Empty<T>(),
            (_, null) => source,
            (null, _) => items,
            (_, _) => addRangeImmutedIterator(source, items)
        };
        static IEnumerable<T> addRangeImmutedIterator(IEnumerable<T> source, IEnumerable<T> items)
        {
            foreach (var item in source)
            {
                yield return item;
            }

            foreach (var item in items)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Determines whether any element exists in the given enumerable.
    /// </summary>
    /// <param name="source">The enumerable to check for elements.</param>
    /// <returns>True if the enumerable contains any elements, otherwise false.</returns>
    public static bool Any([NotNullWhen(true)] this IEnumerable? source) =>
        source switch
        {
            null => false, // If the enumerable is null, no elements exist.
            ICollection collection => collection.Count > 0, // If the enumerable is an ICollection, check its Count property.
            _ => source.GetEnumerator().MoveNext() // Use enumerator to check if any elements exist.
        };

    /// <summary>
    /// Creates an IEnumerable from a given IEnumerable.
    /// </summary>
    /// <param name="source">The source IEnumerable.</param>
    /// <returns>An IEnumerable.</returns>
    public static IEnumerable<T> Iterate<T>(this IEnumerable<T> source)
    {
        //Check if the source is null
        if (source is null)
        {
            //If it is, return an empty IEnumerable
            yield break;
        }
        //Loop through each item in the source
        foreach (var item in source)
        {
            //Return each item in the source
            yield return item;
        }
    }
}