using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.Handlers.Aggregator
{
    [TestFixture]
    public class Constructor_Should
    {

        private Mock<ITaxUtils> taxUtilsMock;
        private Mock<TaxHandler> handler1Mock;
        private Mock<TaxHandler> handler2Mock;

        private List<TaxHandler> handlersList;

        [SetUp]
        public void Init()
        {
            handler1Mock = new Mock<TaxHandler>();
            handler2Mock = new Mock<TaxHandler>();

            handlersList = new List<TaxHandler>() { handler1Mock.Object,
                handler2Mock.Object };

            taxUtilsMock = new Mock<ITaxUtils>();
            taxUtilsMock.Setup(t => t.ValidateHandlers(handlersList));
        }

        [Test]
        public void CreateObjectOfType_TaxationAggregator_WhenParameterIsValid()
        {
            // Arrange Act
            var instance = new TaxationAggregator(handlersList, taxUtilsMock.Object);

            // Assert
            Assert.IsInstanceOf(typeof(TaxationAggregator), instance);
            taxUtilsMock.Verify(t => t.ValidateHandlers(handlersList), Times.Once());
        }

        [Test]
        public void ShouldThrow_ArgumentNullException_WhenTaxUtilsIsNull()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new TaxationAggregator(handlersList, null));
        }

        [Test]
        public void ShouldThrow_Exception_WhenValidateHandlersFail()
        {
            // Arrange
            taxUtilsMock.Setup(u => u.ValidateHandlers(It.IsAny<List<TaxHandler>>()))
                .Throws<Exception>();

            // Act Assert
            Assert.Throws<Exception>(() => new TaxationAggregator(handlersList, taxUtilsMock.Object));
        }
    }
}
