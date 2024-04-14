using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public interface IListDictionary<TKey, TValue> : IList<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        int IndexOf(TKey item);

        int IndexOf(TValue item);
    }
}
