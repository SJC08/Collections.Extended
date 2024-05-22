﻿using Asjc.Extensions;

namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class OrderedDictionaryTests
    {
        public static void RunTest(Action<OrderedDictionary<string, string>> action)
        {
            OrderedDictionary<string, string> od = new()
            {
                { "A", "AAA" },
                { "B", "BBB" },
                { "C", "CCC" }
            };
            action(od);
            Assert.IsTrue(od.Keys.ContentEqual(od.OrderedKeys));
            Assert.IsTrue(od.Values.ContentEqual(od.OrderedValues));
            Assert.IsTrue(od.Count == od.Keys.Count && od.Count == od.Values.Count);
            Assert.IsFalse(od.Keys.HasDuplicates());
        }

        [TestMethod]
        public void Add1_DuplicateKey_ThrowsArgumentException()
        {
            RunTest(od =>
            {
                void a() => od.Add("A", "ZZZ");
                Assert.ThrowsException<ArgumentException>(a);
            });
        }

        [TestMethod]
        public void Add2_DuplicateKey_ThrowsArgumentException()
        {
            RunTest(od =>
            {
                void a() => od.Add(new("A", "ZZZ"));
                Assert.ThrowsException<ArgumentException>(a);
            });
        }

        [TestMethod]
        public void Insert_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            RunTest(od =>
            {
                void a() => od.Insert(-1, new("D", "ZZZ"));
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }

        [TestMethod]
        public void Insert_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            RunTest(od =>
            {
                void a() => od.Insert(od.Count + 1, new("D", "ZZZ"));
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }

        [TestMethod]
        public void Remove1()
        {
            RunTest(od =>
            {
                od.Remove("B");
                Assert.IsFalse(od.ContainsKey("B"));
                Assert.IsFalse(od.Values.Contains("BBB"));
            });
        }

        [TestMethod]
        public void Remove2()
        {
            RunTest(od =>
            {
                od.Remove(new KeyValuePair<string, string>("B", "BBB"));
                Assert.IsFalse(od.ContainsKey("B"));
                Assert.IsFalse(od.Values.Contains("BBB"));
            });
        }

        [TestMethod]
        public void RemoveAt() => RunTest(od => od.RemoveAt(0));

        [TestMethod]
        public void RemoveAt_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            RunTest(od =>
            {
                void a() => od.RemoveAt(-1);
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }

        [TestMethod]
        public void RemoveAt_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            RunTest(od =>
            {
                void a() => od.RemoveAt(od.Count + 1);
                Assert.ThrowsException<ArgumentOutOfRangeException>(a);
            });
        }
    }
}