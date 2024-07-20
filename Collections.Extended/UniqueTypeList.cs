namespace Asjc.Collections.Extended
{
    public class UniqueTypeList : UniqueTypeList<object>
    {
        public UniqueTypeList() : base() { }

        public UniqueTypeList(IEnumerable<KeyValuePair<Type, object>> pairs) : base(pairs) { }

        public UniqueTypeList(IEnumerable<object> values) : base(values) { }
    }

    public class UniqueTypeList<TBase> : KeyedList<Type, TBase>
    {
        public UniqueTypeList() : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj))) { }

        public UniqueTypeList(IEnumerable<KeyValuePair<Type, TBase>> pairs) : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj)), pairs) { }

        public UniqueTypeList(IEnumerable<TBase> values) : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj)), values) { }

        /// <summary>
        /// Gets the element whose key is the type <typeparamref name="T"/>.
        /// </summary>
        public T? Get<T>() where T : TBase => (T?)this[typeof(T)];

        /// <summary>
        /// Sets the element whose key is the type <typeparamref name="T"/>.
        /// </summary>
        public void Set<T>(T value) where T : TBase => this[typeof(T)] = value;
    }
}
