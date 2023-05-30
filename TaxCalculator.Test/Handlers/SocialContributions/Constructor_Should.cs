using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.Handlers.SocialContributions
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateObjectOfType_SocialContributionsHandler_WhenParameterIsValid()
        {
            // Arrange
            var taxationServiceMock = new Mock<ITaxationService>();
            var loggerMock = new Mock<ILogger<SocialContributionsHandler>>();

            // Act
            var instance = new SocialContributionsHandler(taxationServiceMock.Object, loggerMock.Object);

            // Assert
            Assert.IsInstanceOf(typeof(SocialContributionsHandler), instance);
        }

        [Test]
        public void ShouldThrow_ArgumentNullException_WhenParameterIsNull()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new SocialContributionsHandler(null, null));
        }
    }
}
