using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Asjc.Collections.Extended
{
    public class ListDictionary<TKey, TValue> : IListDictionary<TKey, TValue>
    {
        private readonly List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();
        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get => list[index];
            set
            {
                if (!list[index].Key.Equals(value.Key))
                    throw new ArgumentException("The key of the specified index doesn't match the key of the given KeyValuePair.");
                dictionary[value.Key] = value.Value;
                list[index] = value;
            }
        }

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set
            {
                if (ContainsKey(key))
                {
                    dictionary[key] = value;
                    list[IndexOf(key)] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    // Since it doesn't exist, add a new one.
                    Add(key, value);
                }
            }
        }

        public ICollection<TKey> Keys => dictionary.Keys;

        public List<TKey> OrderedKeys => list.Select(kvp => kvp.Key).ToList();

        public ICollection<TValue> Values => dictionary.Values;

        public List<TValue> OrderedValues => list.Select(kvp => kvp.Value).ToList();

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value); // Throws an exception when the key is duplicated.
            list.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.Add(item.Key, item.Value); // Throws an exception when the key is duplicated.
            list.Add(item);
        }

        public void Clear()
        {
            dictionary.Clear();
            list.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => list.Contains(item);

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => list.GetEnumerator();

        public int IndexOf(TKey key) => OrderedKeys.IndexOf(key);

        public int IndexOf(KeyValuePair<TKey, TValue> item) => list.IndexOf(item);

        public void Insert(int index, KeyValuePair<TKey, TValue> item)
        {
            list.Insert(index, item); // Throws an exception when out of range.
            dictionary.Add(item.Key, item.Value);
        }

        public bool Remove(TKey key)
        {
            bool found = dictionary.Remove(key);
            if (found)
                list.RemoveAt(IndexOf(key));
            return found;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool found = list.Remove(item);
            if (found)
                dictionary.Remove(item.Key);
            return found;
        }

        public void RemoveAt(int index)
        {
            var item = list[index]; // Throws an exception when out of range.
            list.RemoveAt(index);
            dictionary.Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
