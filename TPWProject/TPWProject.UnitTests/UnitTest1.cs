using TPWProject;

namespace TPWProject.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var class1 = new Class1();
            Assert.AreEqual(1, class1.return1());
        }
    }
}