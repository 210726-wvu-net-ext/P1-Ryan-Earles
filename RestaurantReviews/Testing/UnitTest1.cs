using NUnit.Framework;

namespace Testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //this method is called before every test is run
        }

        [Test]
        public void Test1()
        {
            //this is the test
            
            Assert.Pass();
        }
    }
}