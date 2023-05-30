using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationServices;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;


namespace TaxCalculator.UnitTests.Handlers.IncomeTax
{
    [TestFixture]
    public class HandleImplementation_Should
    {
        [Test]
        public void Call_GetIncomeTax_And_ReturnDueTax()
        {
            // Arrange
            decimal salaryInput = 3000;
            decimal expectedTax = 200;

            var loggerMock = new Mock<ILogger<IncomeTaxHandler>>();

            var taxationServiceMock = new Mock<ITaxationService>();
            taxationServiceMock.Setup(t => t.GetIncomeTax(salaryInput))
                .Returns(expectedTax);

            var incomeTaxHandler = new IncomeTaxHandler(taxationServiceMock.Object, loggerMock.Object);

            // Act
            var dueTax = incomeTaxHandler.Handle(salaryInput);

            // Assert
            Assert.AreEqual(expectedTax, dueTax);

            taxationServiceMock.Verify(t => t.GetIncomeTax(salaryInput), Times.Once);
            loggerMock.Verify(l => l.Log(
               LogLevel.Information,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((v, t) => v.ToString() == string.Format(TaxMessages.INCOME_TAX_LOG, dueTax)),
               null,
               It.IsAny<Func<It.IsAnyType, Exception, string>>())
            , Times.Once
            );
        }
    }
}
