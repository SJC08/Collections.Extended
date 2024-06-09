using System;

namespace Asjc.Collections.Extended
{
    public class UniqueTypeList : UniqueTypeList<object> { }

    public class UniqueTypeList<T> : KeyedList<Type, T>
    {
        public UniqueTypeList() : base(obj => obj.GetType()) { }

        /// <summary>
        /// Gets the element whose key is the type <typeparamref name="Type"/>.
        /// </summary>
        public Type Get<Type>() where Type : T => (Type)this[typeof(Type)];
    }
}
