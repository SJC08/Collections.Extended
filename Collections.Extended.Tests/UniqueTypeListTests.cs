namespace Asjc.Collections.Extended.Tests
{
    [TestClass]
    public class UniqueTypeListTests
    {
        public static UniqueTypeList UTL => [0, "A", 1.0, 2.0f];

        [TestMethod]
        public void Get()
        {
            Assert.AreEqual(UTL.Get<int>(), 0);
            Assert.AreEqual(UTL.Get<string>(), "A");
            Assert.AreEqual(UTL.Get<double>(), 1.0);
            Assert.AreEqual(UTL.Get<float>(), 2.0f);
        }

        [TestMethod]
        public void Null()
        {
            var utl = UTL;
            utl[typeof(string)] = null;
            Assert.IsNull(utl.Get<string>());
        }
    }
}