using Asjc.Extensions;

namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class OrderedDictionaryTests
    {
        public static void RunTest(Action<OrderedDictionary<string, string>> action)
        {
            OrderedDictionary<string, string> ld = new()
            {
                { "A", "AAA" },
                { "B", "BBB" },
                { "C", "CCC" }
            };
            action(ld);
            Assert.IsTrue(ld.Keys.ContentEqual(ld.OrderedKeys));
            Assert.IsTrue(ld.Values.ContentEqual(ld.OrderedValues));
            Assert.IsTrue(ld.Count == ld.Keys.Count && ld.Count == ld.Values.Count);
            Assert.IsFalse(ld.Keys.HasDuplicates());
        }

        [TestMethod]
        public void Add1_DuplicateKey_ThrowsArgumentException()
        {
            RunTest(ld =>
            {
                void a() => ld.Add("A", "ZZZ");
                Assert.ThrowsException<ArgumentException>(a);
            });
        }

        [TestMethod]
        public void Add2_DuplicateKey_ThrowsArgumentException()
        {
            RunTest(ld =>
            {
                void a() => ld.Add(new("A", "ZZZ"));
                Assert.ThrowsException<ArgumentException>(a);
            });
        }

        [TestMethod]
        public void Insert_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            RunTest(ld =>
            {
                void a() => ld.Insert(-1, new("D", "ZZZ"));
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }

        [TestMethod]
        public void Insert_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            RunTest(ld =>
            {
                void a() => ld.Insert(ld.Count + 1, new("D", "ZZZ"));
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }

        [TestMethod]
        public void Remove1()
        {
            RunTest(ld =>
            {
                ld.Remove("B");
                Assert.IsFalse(ld.ContainsKey("B"));
                Assert.IsFalse(ld.Values.Contains("BBB"));
            });
        }

        [TestMethod]
        public void Remove2()
        {
            RunTest(ld =>
            {
                ld.Remove(new KeyValuePair<string, string>("B", "BBB"));
                Assert.IsFalse(ld.ContainsKey("B"));
                Assert.IsFalse(ld.Values.Contains("BBB"));
            });
        }

        [TestMethod]
        public void RemoveAt() => RunTest(ld => ld.RemoveAt(0));

        [TestMethod]
        public void RemoveAt_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            RunTest(ld =>
            {
                void a() => ld.RemoveAt(-1);
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }

        [TestMethod]
        public void RemoveAt_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            RunTest(ld =>
            {
                void a() => ld.RemoveAt(ld.Count + 1);
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }
    }
}