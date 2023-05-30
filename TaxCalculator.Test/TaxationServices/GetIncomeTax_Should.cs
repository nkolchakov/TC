using NUnit.Framework;
using TaxationServices;

namespace TaxCalculator.UnitTests.TaxationServices
{
    [TestFixture]
    public class GetIncomeTax_Should
    {
        private TaxationService _taxationService;

        [SetUp]
        public void Init()
        {
            this._taxationService = new TaxationService();
        }

        [TestCase(0)]
        [TestCase(Constants.MIN_TAXABLE_INCOME - 10)]
        public void ReturnZero_WhenGrossSalaryUnderIncomeThreshold(decimal belowIncomeThresholdInput)
        {
            // Arragne Act
            decimal actual = _taxationService.GetIncomeTax(belowIncomeThresholdInput);

            // Assert
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void ReturnDueTax_WhenGrossSalaryIsAboveMinThreshold()
        {
            // Arrange
            decimal incomeAboveThreshold = 1100;
            const decimal expectedTax = 10; // 10%

            // Act
            decimal actual = _taxationService.GetIncomeTax(incomeAboveThreshold);

            // Assert
            Assert.AreEqual(expectedTax, actual);
        }
    }
}
