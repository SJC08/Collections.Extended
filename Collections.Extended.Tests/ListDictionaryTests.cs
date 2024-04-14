namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class ListDictionaryTests
    {
        private readonly ListDictionary<int, string> ld = new()
        {
            { 0, "ABC" },
            { 1, "DEF" }
        };

        [TestMethod]
        public void CountConsistency()
        {
            bool equal = ld.Count == ld.Keys.Count &&
                         ld.Count == ld.Values.Count &&
                         ld.Count == ld.OrderedKeys.Count &&
                         ld.Count == ld.OrderedValues.Count;
            Assert.IsTrue(equal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add1_DuplicateKey_ThrowsArgumentException() => ld.Add(0, "XYZ");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add2_DuplicateKey_ThrowsArgumentException() => ld.Add(new(1, "XYZ"));

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Insert_IndexLessThanZero_ThrowsArgumentOutOfRangeException() => ld.Insert(-1, new(2, "XYZ"));

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Insert_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException() => ld.Insert(ld.Count + 1, new(2, "XYZ"));

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAt_IndexLessThanZero_ThrowsArgumentOutOfRangeException() => ld.RemoveAt(-1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAt_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException() => ld.RemoveAt(ld.Count + 1);
    }
}