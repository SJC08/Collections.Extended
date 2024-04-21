using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Asjc.Collections.Extended
{
    public class Shelf<T> : IShelf<T>
    {
        private readonly ListDictionary<object, T> items = new ListDictionary<object, T>();

        public T this[int index]
        {
            get => items[index].Value;
            set => items[index] = new KeyValuePair<object, T>(KeySelector(value), value);
            // set => items[items.OrderedKeys[index]] = value;
        }

        public Func<object, T> KeySelector { get; set; }

        public int Count => items.Count;

        public bool IsReadOnly => items.IsReadOnly;

        public void Add(T item) => items.Add(KeySelector(item), item);

        public void Clear() => items.Clear();

        public bool Contains(T item) => items.OrderedValues.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => items.OrderedValues.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => items.OrderedValues.GetEnumerator();

        public int IndexOf(T item) => items.OrderedValues.IndexOf(item);

        public void Insert(int index, T item)
        {
            var kvp = new KeyValuePair<object, T>(KeySelector(items), item);
            items.Insert(index, kvp);
        }

        public bool Remove(T item)
        {
            var key = items.OrderedKeys.FirstOrDefault(k => items[k].Equals(item));
            return items.Remove(key);
        }
        public void RemoveAt(int index) => items.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
