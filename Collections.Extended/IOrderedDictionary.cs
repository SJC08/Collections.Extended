using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IList<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets an <see cref="List{T}"/> containing the ordered keys of the <see cref="IOrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        List<TKey> OrderedKeys { get; }

        /// <summary>
        /// Gets an <see cref="List{T}"/> containing the ordered values of the <see cref="IOrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        List<TValue> OrderedValues { get; }
    }
}
