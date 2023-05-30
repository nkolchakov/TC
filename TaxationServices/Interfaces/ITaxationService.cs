using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationServices.Interfaces
{
    public interface ITaxationService
    {
        /// <summary>
        /// Calculate the income tax. The taxable amount is only the excess above the minimum threshold.
        /// </summary>
        /// <param name="grossSalary">The Gross salary</param>
        /// <returns>The tax due</returns>
        decimal GetIncomeTax(decimal grossSalary);

        /// <summary>
        /// Calculate the Social contributions. The taxable amounts is the value between a bracket range, above
        /// a given threshold value
        /// </summary>
        /// <param name="grossSalary">The Gross salary</param>
        /// <returns>The social contribution due</returns>
        decimal GetSocialContributions(decimal grossSalary);
    }
}
