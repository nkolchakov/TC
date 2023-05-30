using Moq;
using NUnit.Framework;
using System;
using TaxationServices;
using TaxCalculator.UnitTests.Handlers.Aggregator;

namespace TaxCalculator.UnitTests.Handlers.TaxHandlerBase
{
    [TestFixture]
    public class ValidateIncome_Should
    {
        private Mock<Handler1Stub> handler1Stub;
        private Handler1Stub instance;

        [SetUp]
        public void Init()
        {
            handler1Stub = new Mock<Handler1Stub>() { CallBase = true };
            instance = handler1Stub.Object;
        }

        [TestCase(0)]
        [TestCase(-100)]
        public void Throw_ArgumenOutOfRangeException_WhenInputOutOfRange(decimal invalidRange)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => instance.Handle(invalidRange),
                TaxMessages.INVALID_RANGE
            );
        }

        [Test]
        public void ContinueWithHandle_WhenInputIsInsideRange()
        {
            var validInput = 1500;

            decimal actual = instance.Handle(validInput);

            Assert.AreEqual(1, actual);
        }
    }
}
