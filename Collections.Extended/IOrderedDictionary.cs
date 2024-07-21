using System.Collections;

namespace Asjc.Collections.Extended
{
    /// <summary>
    /// Represents a non-generic collection of key/value pairs that are ordered.
    /// </summary>
    public interface IOrderedDictionary : IDictionary, IList
    {
        /// <summary>
        /// Gets an <see cref="IList"/> containing the ordered keys of the <see cref="IOrderedDictionary"/>.
        /// </summary>
        IList OrderedKeys { get; }

        /// <summary>
        /// Gets an <see cref="IList"/> containing the ordered values of the <see cref="IOrderedDictionary"/>.
        /// </summary>
        IList OrderedValues { get; }
    }

    /// <summary>
    /// Represents a generic collection of key/value pairs that are ordered.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IList<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets an <see cref="IList{T}"/> containing the ordered keys of the <see cref="IOrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        IList<TKey> OrderedKeys { get; }

        /// <summary>
        /// Gets an <see cref="IList{T}"/> containing the ordered values of the <see cref="IOrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        IList<TValue> OrderedValues { get; }
    }
}
