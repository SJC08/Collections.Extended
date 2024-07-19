namespace Asjc.Collections.Extended
{
    public interface IKeyedList<TKey, TValue> : IList<TValue>, IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets the delegate used to extract the key from the value.
        /// </summary>
        Func<TValue, TKey> KeySelector { get; }
    }
}
