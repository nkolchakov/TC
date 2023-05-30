using Microsoft.Extensions.Logging;
using TaxationServices.Interfaces;

namespace TaxationServices.Handlers
{
    public class IncomeTaxHandler : TaxHandler
    {
        private readonly ITaxationService _taxationService;
        private readonly ILogger _logger;
        public IncomeTaxHandler(ITaxationService taxationService, ILogger<IncomeTaxHandler> _logger)
        {
            _taxationService = taxationService ?? throw new ArgumentNullException(nameof(taxationService));
            this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        protected override decimal HandleImplementation(decimal grossValue)
        {
            var tax = this._taxationService.GetIncomeTax(grossValue);

            _logger.LogInformation(string.Format(TaxMessages.INCOME_TAX_LOG, tax));
            return tax;
        }
    }
}
