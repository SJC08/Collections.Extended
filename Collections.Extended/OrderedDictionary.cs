using System.Collections;

namespace Asjc.Collections.Extended
{
    /// <summary>
    /// Represents a generic collection of key/value pairs that are ordered.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public class OrderedDictionary<TKey, TValue> : IOrderedDictionary<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> dictionary = [];
        private readonly List<KeyValuePair<TKey, TValue>> list = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class.
        /// </summary>
        public OrderedDictionary() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedDictionary{TKey, TValue}"/> class that contains elements copied from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            foreach (var item in pairs)
                Add(item);
        }

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get => list[index];
            set
            {
                var current = list[index].Key;
                // For the dictionary, different cases are handled separately.
                if (current.Equals(value.Key))
                {
                    dictionary[value.Key] = value.Value;
                }
                else
                {
                    dictionary.Add(value.Key, value.Value); // Throws an exception when the key is duplicated.
                    dictionary.Remove(current); // Remove the original value.
                }
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
                    list[OrderedKeys.IndexOf(key)] = new KeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    // Since it doesn't exist, add a new one.
                    Add(key, value);
                }
            }
        }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the keys in the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        public ICollection<TKey> Keys => dictionary.Keys;

        /// <summary>
        /// Gets an <see cref="List{T}"/> containing the ordered keys in the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <remarks>
        /// The returned <see cref="List{T}"/> is a copy of the keys in the original dictionary.
        /// </remarks>
        public List<TKey> OrderedKeys => list.Select(kvp => kvp.Key).ToList();

        IList<TKey> IOrderedDictionary<TKey, TValue>.OrderedKeys => OrderedKeys;

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        public ICollection<TValue> Values => dictionary.Values;

        /// <summary>
        /// Gets an <see cref="List{T}"/> containing the ordered values in the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <remarks>
        /// The returned <see cref="List{T}"/> is a copy of the values in the original dictionary.
        /// </remarks>
        public List<TValue> OrderedValues => list.Select(kvp => kvp.Value).ToList();

        IList<TValue> IOrderedDictionary<TKey, TValue>.OrderedValues => OrderedValues;

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

        /// <summary>
        /// Determines whether the <see cref="OrderedDictionary{TKey, TValue}"/> contains a specific key and value.
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/> to locate in the <see cref="OrderedDictionary{TKey, TValue}"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="item"/> is found in the <see cref="OrderedDictionary{TKey, TValue}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item) => list.Contains(item);

        /// <summary>
        /// Determines whether the <see cref="OrderedDictionary{TKey, TValue}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="OrderedDictionary{TKey, TValue}"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="OrderedDictionary{TKey, TValue}"/> contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => list.GetEnumerator();

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
                list.RemoveAt(OrderedKeys.IndexOf(key));
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

        public static implicit operator Dictionary<TKey, TValue>(OrderedDictionary<TKey, TValue> od) => od.dictionary;

        public static implicit operator List<KeyValuePair<TKey, TValue>>(OrderedDictionary<TKey, TValue> od) => od.list;
    }
}
