namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class KeyedListTests
    {
        public static KeyedList<string, string> KL
        {
            get
            {
                KeyedList<string, string> kl = new(v => v[..1])
                {
                    { "A", "AAA" },
                    { "B", "BBB" },
                    { "C", "CCC" }
                };
                return kl;
            }
        }

        [TestMethod]
        public void Add1() => KL.Add("DDD");

        [TestMethod]
        public void Add1_DuplicateKey_ThrowsArgumentException() => Assert.ThrowsException<ArgumentException>(() => KL.Add("AAA"));

        [TestMethod]
        public void Insert1()
        {
            var kl = KL;
            kl.Insert(1, "EEE");
            Assert.IsTrue(kl[1] == "EEE");
        }
    }
}