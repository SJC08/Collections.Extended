namespace Asjc.Collections.Extended
{
    public class UniqueTypeList : UniqueTypeList<object> { }

    public class UniqueTypeList<BaseType> : KeyedList<System.Type, BaseType>
    {
        public UniqueTypeList() : base(obj => obj.GetType()) { }

        /// <summary>
        /// Gets the element whose key is the type <typeparamref name="Type"/>.
        /// </summary>
        public Type Get<Type>() where Type : BaseType => (Type)this[typeof(Type)];

        /// <summary>
        /// Sets the element whose key is the type <typeparamref name="Type"/>.
        /// </summary>
        public void Set<Type>(Type value) where Type : BaseType => this[typeof(Type)] = value;
    }
}
