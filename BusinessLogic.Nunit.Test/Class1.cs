using NUnit.Framework;

namespace BusinessLogic.Nunit.Test
{
    [TestFixture]
    public class TestBusinessLogicWithNunit
    {
        [Test]
        public void AddTwoNumbers()
        {
            // Arrange
            int expected = 5;

            // Act
            int actual = 3 + 2;

            // Assert

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
