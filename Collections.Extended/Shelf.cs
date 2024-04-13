using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Asjc.Collections.Extended
{
    public class Shelf<T> : IShelf<T>
    {
        private readonly OrderedDictionary<object, T> items = new OrderedDictionary<object, T>();

        public Shelf() : this(i => i.GetType().Name) { }

        public Shelf(Func<T, object> selector) => Selector = selector;

        public T this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public T this[object key]
        {
            get => items[key];
            set => items[key] = value;
        }

        public Func<T, object> Selector { get; }

        public int Count => items.Count;

        public bool IsReadOnly => false;

        public void Add(T item) => items.Add(Selector(item), item);

        public void Clear() => items.Clear();

        public bool Contains(T item) => items.Values.Contains(item);

        public bool Contains(object key) => items.ContainsKey(key);

        public void CopyTo(T[] array, int arrayIndex) => items.Values.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => items.Values.GetEnumerator();

        public int IndexOf(T item) => items.Values.ToList().IndexOf(item);

        public int IndexOf(object key) => items.IndexOfKey(key);

        public void Insert(int index, T item) => items.Insert(index, Selector(item), item);

        public bool Remove(T item) => items.Remove(Selector(item));

        public void RemoveAt(int index) => items.RemoveAt(index);

        public bool TryAdd(T item)
        {
            var key = Selector(item);
            if (!items.ContainsKey(key))
            {
                items.Add(key, item);
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
