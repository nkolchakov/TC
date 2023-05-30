using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.IncomeTaxHandlerTest
{
    [TestFixture]
    public class Constructor_Should
    {

        [Test]
        public void CreateObjectOfType_IncomeTaxHandler_WhenParameterIsValid()
        {
            // Arrange
            var taxationServiceMock = new Mock<ITaxationService>();
            var loggerMock = new Mock<ILogger<IncomeTaxHandler>>();

            // Act
            var instance = new IncomeTaxHandler(taxationServiceMock.Object, loggerMock.Object);

            // Assert
            Assert.IsInstanceOf(typeof(IncomeTaxHandler), instance);

        }

        [Test]
        public void ShouldThrow_ArgumentNullException_WhenParameterIsNull()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new IncomeTaxHandler(null, null));
        }
    }
}
