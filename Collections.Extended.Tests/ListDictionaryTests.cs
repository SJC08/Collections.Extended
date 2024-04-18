using Asjc.Extensions;

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

        public void Validate(ListDictionary<string, string> ld)
        {
            Assert.IsTrue(ld.Keys.ContentEqual(ld.OrderedKeys));
            Assert.IsTrue(ld.Values.ContentEqual(ld.OrderedValues));
            Assert.IsTrue(ld.Count == ld.Keys.Count && ld.Count == ld.Values.Count);
        }

        [TestMethod]
        public void Add1_DuplicateKey_ThrowsArgumentException()
        {
            var ld = LD;
            var a = () => ld.Add("A", "ZZZ");
            Assert.ThrowsException<ArgumentException>(a);
            Validate(ld);
        }

        [TestMethod]
        public void Add2_DuplicateKey_ThrowsArgumentException()
        {
            var ld = LD;
            var a = () => ld.Add(new("A", "ZZZ"));
            Assert.ThrowsException<ArgumentException>(a);
            Validate(ld);
        }

        [TestMethod]
        public void Insert_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var ld = LD;
            var a = () => ld.Insert(-1, new("D", "ZZZ"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            Validate(ld);
        }

        [TestMethod]
        public void Insert_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            var ld = LD;
            var a = () => ld.Insert(ld.Count + 1, new("D", "ZZZ"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            Validate(ld);
        }

        [TestMethod]
        public void Remove1()
        {
            var ld = LD;
            ld.Remove("B");
            Assert.IsFalse(ld.Keys.Contains("B"));
            Assert.IsFalse(ld.Values.Contains("BBB"));
            Validate(ld);
        }

        [TestMethod]
        public void Remove2()
        {
            var ld = LD;
            ld.Remove(new KeyValuePair<string, string>("B", "BBB"));
            Assert.IsFalse(ld.Keys.Contains("B"));
            Assert.IsFalse(ld.Values.Contains("BBB"));
            Validate(ld);
        }

        [TestMethod]
        public void RemoveAt()
        {
            var ld = LD;
            ld.RemoveAt(0);
            Validate(ld);
        }

        [TestMethod]
        public void RemoveAt_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var ld = LD;
            var a = () => ld.RemoveAt(-1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            Validate(ld);
        }

        [TestMethod]
        public void RemoveAt_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            var ld = LD;
            var a = () => ld.RemoveAt(ld.Count + 1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            Validate(ld);
        }
    }
}