using TaxationServices.Interfaces;

namespace TaxationServices
{
    public class TaxationService : ITaxationService
    {
        public decimal GetIncomeTax(decimal grossSalary)
        {
            decimal tax = 0;

            if (grossSalary >= Constants.MIN_TAXABLE_INCOME)
            {
                decimal taxable = grossSalary - Constants.MIN_TAXABLE_INCOME;
                tax = taxable * Constants.INCOME_TAX_PERCENTAGE / 100;
            }
            return tax;
        }

        public decimal GetSocialContributions(decimal grossSalary)
        {
            /**
             * if income >= 1000:
             *      taxable = Math.Min(3000,income) - 1000
             * else:
             *      no taxation
             */

            decimal scTax = 0;
            if (grossSalary >= Constants.SC_MIN_TAXABLE_INCOME)
            {
                var taxableIncome =
                    Math.Min(Constants.SC_MAX_TAXABLE_INCOME, grossSalary) - Constants.SC_MIN_TAXABLE_INCOME;
                scTax = taxableIncome * Constants.SC_TAX_PERCENTAGE / 100;
            }
            return scTax;
        }
    }
}