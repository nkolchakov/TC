using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using TaxationServices;
using TaxationServices.IO.Interfaces;
using TaxCalculator.UnitTests.Handlers.Aggregator;

namespace TaxCalculator.UnitTests.TaxationCalculatorTest
{
    [TestFixture]
    public class Start_Should
    {
        private Mock<IReader> _readerMock;
        private Mock<ILogger<TaxationCalculator>> _loggerMock;
        private TaxationCalculator _taxCalculator;
        private Mock<Handler1Stub> _aggrStub;

        [SetUp]
        public void Init()
        {
            _readerMock = new Mock<IReader>();
            _loggerMock = new Mock<ILogger<TaxationCalculator>>();
            _aggrStub = new Mock<Handler1Stub>() { CallBase = true };

            _taxCalculator = new TaxationCalculator(
                _aggrStub.Object,
                _loggerMock.Object,
                _readerMock.Object);
        }

        private void VerifyLoggerWrapper(
            string message,
            Times times,
            LogLevel level = LogLevel.Information)
        {
            this._loggerMock.Verify(x => x.Log(
              level,
              It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((v, t) => v.ToString() == message),
              null,
              It.IsAny<Func<It.IsAnyType, Exception, string>>()),
          times);
        }

        [Test]
        public void LogsNetIncome_OnValidInput()
        {
            //successfull scenario
            //   - log is called,
            //   - reader is called,
            //   - aggregator.Handle is called,
            //   - net income is logged,
            //   - true is returned

            // Arrange
            decimal grossIncome = 2000;
            decimal expectedNet = grossIncome - _aggrStub.Object.ExpectedResult;
            _readerMock.SetupSequence(r => r.ReadLine())
                .Returns(grossIncome.ToString())
                .Returns("end");

            // Act
            bool didEnd = _taxCalculator.Start();

            // Assert

            VerifyLoggerWrapper(
                TaxMessages.SEPARATOR_INFO,
                Times.Once()
                );

            // two times, on first input and before prompting the end command.
            VerifyLoggerWrapper(
                string.Format(TaxMessages.MESSAGE_INPUT, TaxMessages.CURRENCY_SYMBOL),
                Times.Exactly(2)
                );

            // same as above
            _readerMock.Verify(r => r.ReadLine(), Times.Exactly(2));

            // can't verify against Handle directly, because its non-abstract.
            // Verify against inner implementation (called on Handle)
            _aggrStub.Protected().Verify(
                "HandleImplementation",
                Times.Once(),
                grossIncome
             );

            VerifyLoggerWrapper(
                string.Format(TaxMessages.NET_INCOME_LOG, expectedNet),
                Times.Once());
            Assert.IsTrue(didEnd);
        }

        [Test]
        public void RepeatsSuccessfully_OnValidInputs_TillEndCommand()
        {
            // repeats successfully on few commands till end hits

            // Arrange
            decimal grossIncome = 2000;
            decimal expectedNet = grossIncome - _aggrStub.Object.ExpectedResult;

            _readerMock.SetupSequence(r => r.ReadLine())
                .Returns(grossIncome.ToString())
                .Returns(grossIncome.ToString())
                .Returns(grossIncome.ToString())
                .Returns("end");

            // Act
            bool didEnd = _taxCalculator.Start();

            // Assert
            VerifyLoggerWrapper(
                string.Format(TaxMessages.NET_INCOME_LOG, expectedNet),
                Times.Exactly(3)
                );
            Assert.IsTrue(didEnd);
        }

        [Test]
        public void Repeats_OnNonParsableValue()
        {
            //repeats on non parsable 

            // Arrange
            string invalidDecimalInput = "invalid-decimal";

            _readerMock.SetupSequence(r => r.ReadLine())
                .Returns(invalidDecimalInput)
                .Returns("end");

            // Act
            bool didEnd = _taxCalculator.Start();

            // Assert
            VerifyLoggerWrapper(
                TaxMessages.NON_PARSABLE_INPUT,
                Times.Once(),
                LogLevel.Error);

            Assert.IsTrue(didEnd);
        }

        [Test]
        public void Repeats_OnNegativeSalary()
        {
            // repeats on negative salary value

            // Arrange
            decimal negativeSalart = -10;

            _readerMock.SetupSequence(r => r.ReadLine())
                .Returns(negativeSalart.ToString())
                .Returns("end");

            // Act
            bool didEnd = _taxCalculator.Start();

            // Assert
            VerifyLoggerWrapper(
                TaxMessages.INVALID_RANGE,
                Times.Once(),
                LogLevel.Error
            );

            Assert.IsTrue(didEnd);
        }
    }
}
