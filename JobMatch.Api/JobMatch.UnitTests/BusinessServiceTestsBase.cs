
using NUnit.Framework;

namespace JobMatch.UnitTests
{
    [TestFixture]
    public class BusinessServiceTestsBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestsShouldRun()
        {
            Assert.AreEqual(1,1);
        }
    }
}
