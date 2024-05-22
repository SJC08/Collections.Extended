using System;
using System.Collections;
using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public class KeyedList<T> : KeyedList<object, T>
    {
        public KeyedList() : this(k => k.GetType()) { } // To be determined.

        public KeyedList(Func<T, object> keySelector) : base(keySelector) { }
    }

    public class KeyedList<TKey, TValue> : IKeyedList<TKey, TValue>
    {
        private readonly OrderedDictionary<TKey, TValue> items = new OrderedDictionary<TKey, TValue>();

        public KeyedList(Func<TValue, TKey> keySelector) => KeySelector = keySelector;

        public TValue this[int index]
        {
            get => items[index].Value;
            set => items[index] = new KeyValuePair<TKey, TValue>(KeySelector(value), value);
        }

        public TValue this[TKey key]
        {
            get => items[key];
            set => items[key] = value;
        }

        public Func<TValue, TKey> KeySelector { get; }

        public int Count => items.Count;

        public bool IsReadOnly => items.IsReadOnly;

        public ICollection<TKey> Keys => items.Keys;

        public ICollection<TValue> Values => items.Values;

        public void Add(TValue item) => items.Add(KeySelector(item), item);

        public void Add(TKey key, TValue value) => items.Add(key, value);

        public void Add(KeyValuePair<TKey, TValue> item) => items.Add(item);

        public void Clear() => items.Clear();

        public bool Contains(TValue item) => items.Values.Contains(item);

        public bool Contains(KeyValuePair<TKey, TValue> item) => items.Contains(item);

        public bool ContainsKey(TKey key) => items.ContainsKey(key);

        public void CopyTo(TValue[] array, int arrayIndex) => items.OrderedValues.CopyTo(array, arrayIndex);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        public IEnumerator<TValue> GetEnumerator() => items.OrderedValues.GetEnumerator();

        public int IndexOf(TValue item) => items.OrderedValues.IndexOf(item);

        public void Insert(int index, TValue item) => items.Insert(index, new KeyValuePair<TKey, TValue>(KeySelector(item), item));

        public bool Remove(TValue item) => items.Remove(items[IndexOf(item)]);

        public bool Remove(TKey key) => items.Remove(key);

        public bool Remove(KeyValuePair<TKey, TValue> item) => items.Remove(item);

        public void RemoveAt(int index) => items.RemoveAt(index);

        public bool TryGetValue(TKey key, out TValue value) => items.TryGetValue(key, out value);

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator List<TValue>(KeyedList<TKey, TValue> kl) => kl.items.OrderedValues;
        public static implicit operator Dictionary<TKey, TValue>(KeyedList<TKey, TValue> kl) => kl.items;
    }
}
