using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.Handlers.Aggregator
{
    [TestFixture]
    public class HandleImplementation_Should
    {
        [Test]
        public void CallHandle_ForEachHandler()
        {
            // Arrange
            decimal grossIncome = 3000;

            var taxUtilsMock = new Mock<ITaxUtils>();
            var handler1Mock = new Mock<Handler1Stub>() { CallBase = true };
            var handler2Mock = new Mock<Handler2Stub>() { CallBase = true };

            var expected = handler1Mock.Object.ExpectedResult + handler2Mock.Object.ExpectedResult;
            var handlers = new List<TaxHandler>() { handler1Mock.Object, handler2Mock.Object };
            var aggregator = new TaxationAggregator(handlers, taxUtilsMock.Object);

            // Act
            decimal totalTax = aggregator.Handle(grossIncome);

            // Assert
            handler1Mock.Protected().Verify("HandleImplementation", Times.Once(), grossIncome);
            handler2Mock.Protected().Verify("HandleImplementation", Times.Once(), grossIncome);
            Assert.AreEqual(expected, totalTax);
        }
    }
}
