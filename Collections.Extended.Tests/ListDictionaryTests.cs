namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class ListDictionaryTests
    {
        public static ListDictionary<string, string> LD => new()
        {
            { "A", "AAA" },
            { "B", "BBB" },
            { "C", "CCC" }
        };

        [TestMethod]
        public void CountConsistency()
        {
            bool equal = LD.Count == LD.Keys.Count &&
                         LD.Count == LD.Values.Count &&
                         LD.Count == LD.OrderedKeys.Count &&
                         LD.Count == LD.OrderedValues.Count;
            Assert.IsTrue(equal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add1_DuplicateKey_ThrowsArgumentException() => LD.Add("A", "ZZZ");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add2_DuplicateKey_ThrowsArgumentException() => LD.Add(new("A", "ZZZ"));

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Insert_IndexLessThanZero_ThrowsArgumentOutOfRangeException() => LD.Insert(-1, new("D", "ZZZ"));

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Insert_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException() => LD.Insert(LD.Count + 1, new("D", "ZZZ"));

        [TestMethod]
        public void Remove1()
        {
            var ld = LD;
            ld.Remove("B");
            Assert.IsFalse(ld.Keys.Contains("B"));
            Assert.IsFalse(ld.OrderedKeys.Contains("B"));
            Assert.IsFalse(ld.Values.Contains("BBB"));
            Assert.IsFalse(ld.OrderedValues.Contains("BBB"));
        }

        [TestMethod]
        public void Remove2()
        {
            var ld = LD;
            ld.Remove(new KeyValuePair<string, string>("B", "BBB"));
            Assert.IsFalse(ld.Keys.Contains("B"));
            Assert.IsFalse(ld.OrderedKeys.Contains("B"));
            Assert.IsFalse(ld.Values.Contains("BBB"));
            Assert.IsFalse(ld.OrderedValues.Contains("BBB"));
        }

        [TestMethod]
        public void RemoveAt()
        {
            var ld = LD;
            ld.RemoveAt(0);
            Assert.IsTrue(ld.Keys.SequenceEqual(ld.OrderedKeys));
            Assert.IsTrue(ld.Values.SequenceEqual(ld.OrderedValues));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAt_IndexLessThanZero_ThrowsArgumentOutOfRangeException() => LD.RemoveAt(-1);

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAt_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException() => LD.RemoveAt(LD.Count + 1);
    }
}