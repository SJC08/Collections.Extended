using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public interface IOrderedDictionary<TKey, TValue> : IList<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        List<TKey> OrderedKeys { get; }

        List<TValue> OrderedValues { get; }
    }
}
