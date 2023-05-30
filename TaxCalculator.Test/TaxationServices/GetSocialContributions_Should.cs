using NUnit.Framework;
using TaxationServices;

namespace TaxCalculator.UnitTests.TaxationServices
{
    [TestFixture]
    public class GetSocialContributions_Should
    {
        private TaxationService _taxService;
        [SetUp]
        public void Init()
        {
            this._taxService = new TaxationService();
        }

        [TestCase(0)]
        [TestCase(Constants.SC_MIN_TAXABLE_INCOME - 10)]
        public void ReturnZero_WhenGrossSalaryUnderIncomeThreshold(decimal belowIncomeThreshold)
        {
            decimal actual = _taxService.GetSocialContributions(belowIncomeThreshold);

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void ReturnsDueTax_WhenGrossSalaryAboveMinIncomeThreshold_InsideRange()
        {
            // arrange
            decimal insideRangeIncome = 2700; // 1700 taxable (above 1000, up to 3000)
            decimal expected = 255; // 15% of 1700

            // act
            decimal actual = _taxService.GetSocialContributions(insideRangeIncome);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReturnsDueTax_WhenGrossSalaryAboveMinIncomeThreshold_OutsideRange()
        {
            // arrange
            decimal outsideRangeIncome = 5000; // taxable = 2000 (above 1000, not exceeding 3000)
            decimal expected = 300; // 15% of 2000

            // act
            decimal actual = _taxService.GetSocialContributions(outsideRangeIncome);

            // assert
            Assert.AreEqual(actual, expected);
        }
    }
}
