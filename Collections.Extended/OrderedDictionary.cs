using System.Collections;

namespace Asjc.Collections.Extended
{
    /// <inheritdoc cref="IOrderedDictionary{TKey, TValue}"/>
    public class OrderedDictionary<TKey, TValue> : IOrderedDictionary<TKey, TValue>, IOrderedDictionary where TKey : notnull
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        object IDictionary.this[object key]
        {
            get => ((IDictionary)dictionary)[key];
            set => this[(TKey)key] = (TValue)value;
        }

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (KeyValuePair<TKey, TValue>)value;
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

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        public int Count => list.Count;

        /// <summary>
        /// Gets a value that indicates whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        IList<TKey> IOrderedDictionary<TKey, TValue>.OrderedKeys => OrderedKeys;

        IList IOrderedDictionary.OrderedKeys => OrderedKeys;

        IList<TValue> IOrderedDictionary<TKey, TValue>.OrderedValues => OrderedValues;

        IList IOrderedDictionary.OrderedValues => OrderedValues;

        bool IDictionary.IsFixedSize => false;

        bool IList.IsFixedSize => false;

        ICollection IDictionary.Keys => dictionary.Keys;

        ICollection IDictionary.Values => dictionary.Values;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value); // Throws an exception when the key is duplicated.
            list.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="item">The element to add.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.Add(item.Key, item.Value); // Throws an exception when the key is duplicated.
            list.Add(item);
        }

        /// <summary>
        /// Removes all keys and values from the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
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

        /// <summary>
        /// Copies the elements of the <see cref="OrderedDictionary{TKey, TValue}"/> to an array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="OrderedDictionary{TKey, TValue}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}.Enumerator"/> for the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => list.GetEnumerator();

        /// <summary>
        /// Determines the index of a specific item in the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="OrderedDictionary{TKey, TValue}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(KeyValuePair<TKey, TValue> item) => list.IndexOf(item);

        /// <summary>
        /// Inserts an item to the <see cref="OrderedDictionary{TKey, TValue}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="OrderedDictionary{TKey, TValue}"/>.</param>
        public void Insert(int index, KeyValuePair<TKey, TValue> item)
        {
            list.Insert(index, item); // Throws an exception when out of range.
            dictionary.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><see langword="true"/> if the element is successfully found and removed; otherwise, <see langword="false"/>. This method returns <see langword="false"/> if <paramref name="key"/> is not found in the <see cref="OrderedDictionary{TKey, TValue}"/>.</returns>
        public bool Remove(TKey key)
        {
            bool found = dictionary.Remove(key);
            if (found)
                list.RemoveAt(OrderedKeys.IndexOf(key));
            return found;
        }

        /// <summary>
        /// Removes the specified value from the <see cref="OrderedDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The element to remove.</param>
        /// <returns><see langword="true"/> if the element was successfully found and removed; otherwise, <see langword="false"/>. This method returns <see langword="false"/> if <paramref name="item"/> is not found in the <see cref="OrderedDictionary{TKey, TValue}"/>.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool found = list.Remove(item);
            if (found)
                dictionary.Remove(item.Key);
            return found;
        }

        /// <summary>
        /// Removes the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            var item = list[index]; // Throws an exception when out of range.
            list.RemoveAt(index);
            dictionary.Remove(item.Key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="key"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        void IDictionary.Add(object key, object value) => Add((TKey)key, (TValue)value);

        int IList.Add(object value)
        {
            Add((KeyValuePair<TKey, TValue>)value);
            return Count - 1;
        }

        bool IDictionary.Contains(object key) => key is TKey k && ContainsKey(k);

        bool IList.Contains(object value) => value is KeyValuePair<TKey, TValue> i && Contains(i);

        void ICollection.CopyTo(Array array, int index) => ((ICollection)list).CopyTo(array, index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IDictionaryEnumerator IDictionary.GetEnumerator() => dictionary.GetEnumerator();

        int IList.IndexOf(object value)
        {
            if (value is KeyValuePair<TKey, TValue> i)
                return IndexOf(i);
            return -1;
        }

        void IList.Insert(int index, object value) => Insert(index, (KeyValuePair<TKey, TValue>)value);

        void IDictionary.Remove(object key)
        {
            if (key is TKey i)
                Remove(i);
        }

        void IList.Remove(object value)
        {
            if (value is KeyValuePair<TKey, TValue> i)
                Remove(i);
        }

        /// <summary>
        /// Converts the specified <see cref="OrderedDictionary{TKey, TValue}"/> to a <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <remarks>
        /// Changes to the return value are not reflected in the original dictionary.
        /// </remarks>
        public static implicit operator Dictionary<TKey, TValue>(OrderedDictionary<TKey, TValue> od) => new(od.dictionary);

        /// <summary>
        /// Converts the specified <see cref="OrderedDictionary{TKey, TValue}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <remarks>
        /// Changes to the return value are not reflected in the original dictionary.
        /// </remarks>
        public static implicit operator List<KeyValuePair<TKey, TValue>>(OrderedDictionary<TKey, TValue> od) => new(od.list);
    }
}
