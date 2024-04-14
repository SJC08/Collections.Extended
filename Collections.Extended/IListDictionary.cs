using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public interface IListDictionary<TKey, TValue> : IList<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        List<TKey> OrderedKeys { get; }

        List<TValue> OrderedValues { get; }

        int IndexOf(TKey item);

        int IndexOf(TValue item);
    }
}
