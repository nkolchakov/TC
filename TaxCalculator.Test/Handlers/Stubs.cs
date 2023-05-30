using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationServices.Interfaces;

namespace TaxCalculator.UnitTests.Handlers.Aggregator
{

    public class Handler1Stub : TaxHandler
    {
        public decimal ExpectedResult { get; private set; } = 1;
        protected override decimal HandleImplementation(decimal grossValue)
        {
            return ExpectedResult;
        }
    }

    public class Handler2Stub : TaxHandler
    {
        public decimal ExpectedResult { get; private set; } = 2;

        protected override decimal HandleImplementation(decimal grossValue)
        {
            return ExpectedResult;
        }
    }
}
