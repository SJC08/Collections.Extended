using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public interface IShelf<T> : IList<T>
    {
        T this[object key] { get; set; }

        bool Contains(object key);

        int IndexOf(object key);

        bool TryAdd(T item);
    }
}
