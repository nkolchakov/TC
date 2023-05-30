using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaxationServices;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.TaxUtilsTest
{
    [TestFixture]
    public class ValidateHandlers_Should
    {
        private readonly Mock<TaxHandler> _handler1Mock = new Mock<TaxHandler>();
        private readonly Mock<TaxHandler> _handler2Mock = new Mock<TaxHandler>();
        private ITaxUtils _taxUtils;

        [SetUp]
        public void Init()
        {
            this._taxUtils = new TaxUtils();
        }

        [Test]
        public void Throws_ArgumentException_WhenHandlersListIsEmpty()
        {
            var emptyHandlersList = new List<TaxHandler>();

            Assert.Throws<ArgumentException>(
                    () => _taxUtils.ValidateHandlers(emptyHandlersList),
                    TaxMessages.NO_CHAINED_HANDLERS);
        }

        public void Throws_ArgumentNullException_WhenAnyHandlerIsNull()
        {
            var containsNullHandler = new List<TaxHandler>() {
                _handler1Mock.Object,
                null
            };

            Assert.Throws<ArgumentNullException>(
                    () => _taxUtils.ValidateHandlers(containsNullHandler),
                    TaxMessages.CHAINED_HANDLER_NOT_REGISTERED);
        }

        [Test]
        public void Pass_When_HandlersAreValid()
        {
            // arrange
            var validHandlers = new List<TaxHandler>() {
                _handler1Mock.Object,
                _handler2Mock.Object
            };

            // act assert
            Assert.DoesNotThrow(() => _taxUtils.ValidateHandlers(validHandlers));
        }
    }
}
