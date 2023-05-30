using Microsoft.Extensions.Logging;
using TaxationServices.Interfaces;
using TaxationServices.IO.Interfaces;

namespace TaxationServices
{
    public class TaxationCalculator : ITaxationCalculator
    {
        private readonly TaxHandler _taxAggregator;
        private readonly ILogger _logger;
        private readonly IReader _reader;

        public TaxationCalculator(
            TaxHandler aggregator,
            ILogger<TaxationCalculator> logger,
            IReader reader)
        {
            _taxAggregator = aggregator ?? throw new ArgumentNullException(nameof(aggregator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        private string? GetInput()
        {
            this._logger.LogInformation(string.Format(TaxMessages.MESSAGE_INPUT, TaxMessages.CURRENCY_SYMBOL));
            return _reader.ReadLine()?.ToLower();
        }

        public bool Start()
        {
            _logger.LogInformation(TaxMessages.SEPARATOR_INFO);
            string? input;
            while ((input = GetInput()) != TaxMessages.END_COMMAND.ToLower())
            {
                bool isParsed = decimal.TryParse(input, out decimal grossIncome);
                if (!isParsed)
                {
                    _logger.LogError(TaxMessages.NON_PARSABLE_INPUT);
                    continue;
                }

                decimal totalTax;
                try
                {
                    totalTax = _taxAggregator.Handle(grossIncome);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    _logger.LogError(ex.Message);
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"General exception occured: {ex.Message}");
                    continue;
                }

                decimal netIncome = grossIncome - totalTax;
                _logger.LogInformation(string.Format(TaxMessages.NET_INCOME_LOG, netIncome));
            }

            return true;
        }
    }
}
