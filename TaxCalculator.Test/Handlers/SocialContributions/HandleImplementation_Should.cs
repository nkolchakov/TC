using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using TaxationServices;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.Handlers.SocialContributions
{
    [TestFixture]
    public class HandleImplementation_Should
    {
        [Test]
        public void Call_GetSocialContributions_And_ReturnDueTax()
        {
            // Arrange
            decimal salaryInput = 3000;
            decimal expectedTax = 300;

            var loggerMock = new Mock<ILogger<SocialContributionsHandler>>();

            var taxationServiceMock = new Mock<ITaxationService>();
            taxationServiceMock.Setup(t => t.GetSocialContributions(salaryInput))
                .Returns(expectedTax);

            var scHandler = new SocialContributionsHandler(taxationServiceMock.Object, loggerMock.Object);

            // Act
            var dueTax = scHandler.Handle(salaryInput);

            // Assert
            Assert.AreEqual(expectedTax, dueTax);

            taxationServiceMock.Verify(t => t.GetSocialContributions(salaryInput), Times.Once);
            loggerMock.Verify(l => l.Log(
               LogLevel.Information,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((v, t) => v.ToString() == string.Format(TaxMessages.SOCIAL_CONTRIBUTION_LOG, dueTax)),
               null,
               It.IsAny<Func<It.IsAnyType, Exception, string>>())
            , Times.Once
            );
        }
    }
}
