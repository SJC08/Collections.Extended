using System.Collections;

namespace Asjc.Collections.Extended
{
    public class KeyedList<TValue> : KeyedList<object, TValue>
    {
        public KeyedList() : base() { }

        public KeyedList(IEnumerable<KeyValuePair<object, TValue>> pairs) : base(pairs) { }

        public KeyedList(IEnumerable<TValue> values) : base(values) { }

        public KeyedList(Func<TValue, object> keySelector) : base(keySelector) { }

        public KeyedList(Func<TValue, object> keySelector, IEnumerable<KeyValuePair<object, TValue>> pairs) : base(keySelector, pairs) { }

        public KeyedList(Func<TValue, object> keySelector, IEnumerable<TValue> values) : base(keySelector, values) { }
    }

    public class KeyedList<TKey, TValue> : OrderedDictionary<TKey, TValue>, IKeyedList<TKey, TValue> where TKey : notnull
    {
        public KeyedList()
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        public KeyedList(IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        public KeyedList(IEnumerable<TValue> values)
        {
            KeySelector = _ => throw new NotSupportedException();
            foreach (var item in values)
                Add(item);
        }

        public KeyedList(Func<TValue, TKey> keySelector)
        {
            KeySelector = keySelector;
        }

        public KeyedList(Func<TValue, TKey> keySelector, IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
        {
            KeySelector = keySelector;
        }

        public KeyedList(Func<TValue, TKey> keySelector, IEnumerable<TValue> values)
        {
            KeySelector = keySelector;
            foreach (var item in values)
                Add(item);
        }

        public new TValue this[int index]
        {
            get => base[index].Value;
            set => base[index] = new KeyValuePair<TKey, TValue>(KeySelector(value), value);
        }

        /// <inheritdoc/>
        public Func<TValue, TKey> KeySelector { get; set; }

        public void Add(TValue item) => Add(KeySelector(item), item);

        public bool Contains(TValue item) => Values.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex) => OrderedValues.CopyTo(array, arrayIndex);

        public new IEnumerator<TValue> GetEnumerator() => OrderedValues.GetEnumerator();

        public int IndexOf(TValue item) => OrderedValues.IndexOf(item);

        public void Insert(int index, TValue item) => Insert(index, new KeyValuePair<TKey, TValue>(KeySelector(item), item));

        public bool Remove(TValue item) => Remove(base[IndexOf(item)]);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="KeyedList{TKey, TValue}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}.Enumerator"/> for the <see cref="KeyedList{TKey, TValue}"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Converts the specified <see cref="KeyedList{TKey, TValue}"/> to a <see cref="List{T}"/>.
        /// </summary>
        public static implicit operator List<TValue>(KeyedList<TKey, TValue> kl) => kl.OrderedValues;
    }
}
