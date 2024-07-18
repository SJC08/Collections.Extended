namespace Asjc.Collections.Extended
{
    public interface IKeyedList<TKey, TValue> : IList<TValue>, IDictionary<TKey, TValue>
    {
        Func<TValue, TKey> KeySelector { get; }
    }
}
