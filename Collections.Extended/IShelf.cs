using System;
using System.Collections.Generic;

namespace Asjc.Collections.Extended
{
    public interface IShelf<T> : IList<T>
    {
        Func<object, T> KeySelector { get; set; }
    }
}
