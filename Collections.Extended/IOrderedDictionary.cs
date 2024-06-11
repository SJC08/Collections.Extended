using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    // A little bit different from System.Collections.Specialized.OrderedDictionary.
    // I'm not sure if this is more appropriate.
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
