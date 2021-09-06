using NUnit.Framework;
using RestaurantReviews.Domain;

namespace Testing
{
    public class Tests
    {
        private object exception;

        [SetUp]
        public void Setup()
        {
            //this method is called before every test is run
        }

        [Test]
        public void Test1()
        {
            //this is the test
           
            var restaurant = new Restaurant { Name = "Whacking", Rating = 5, Zipcode = 80301 };
            exception = null;
            Assert.AreNotSame(restaurant, exception);
        }
    }
}