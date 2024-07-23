namespace Asjc.Collections.Extended
{
    /// <summary>
    /// Represents a special list that can also be accessed by key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the list.</typeparam>
    /// <typeparam name="TValue">The type of keys in the list.</typeparam>
    public interface IKeyedList<TKey, TValue> : IList<TValue>, IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets the delegate used to extract the key from the value.
        /// </summary>
        Func<TValue, TKey> KeySelector { get; }
    }
}
