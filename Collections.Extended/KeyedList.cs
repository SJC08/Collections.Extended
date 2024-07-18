using System.Collections;

namespace Asjc.Collections.Extended
{
    public class KeyedList<TValue> : KeyedList<object, TValue>
    {
        public KeyedList(Func<TValue, object> keySelector) : base(keySelector) { }

        public KeyedList(Func<TValue, object> keySelector, IEnumerable<KeyValuePair<object, TValue>> pairs) : base(keySelector, pairs) { }

        public KeyedList(Func<TValue, object> keySelector, IEnumerable<TValue> values) : base(keySelector, values) { }
    }

    public class KeyedList<TKey, TValue> : OrderedDictionary<TKey, TValue>, IKeyedList<TKey, TValue> where TKey : notnull
    {
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

        public Func<TValue, TKey> KeySelector { get; }

        public void Add(TValue item) => Add(KeySelector(item), item);

        public bool Contains(TValue item) => Values.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex) => OrderedValues.CopyTo(array, arrayIndex);

        public new IEnumerator<TValue> GetEnumerator() => OrderedValues.GetEnumerator();

        public int IndexOf(TValue item) => OrderedValues.IndexOf(item);

        public void Insert(int index, TValue item) => Insert(index, new KeyValuePair<TKey, TValue>(KeySelector(item), item));

        public bool Remove(TValue item) => Remove(base[IndexOf(item)]);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator List<TValue>(KeyedList<TKey, TValue> kl) => kl.OrderedValues;
    }
}
