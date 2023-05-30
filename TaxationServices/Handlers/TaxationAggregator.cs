using TaxationServices.Interfaces;

namespace TaxationServices.Handlers
{
    /// <summary>
    /// Aggregates all of the Handlers into a chain and accumulate the result from each one into total tax.
    /// </summary>
    public class TaxationAggregator : TaxHandler
    {
        private readonly List<TaxHandler> _handlers;
        private readonly ITaxUtils _taxUtils;
        public TaxationAggregator(IEnumerable<TaxHandler> handlers, ITaxUtils taxUtils)
        {
            _taxUtils = taxUtils ?? throw new ArgumentNullException(nameof(taxUtils));

            _taxUtils.ValidateHandlers(handlers);
            _handlers = handlers.ToList();
        }

        protected override decimal HandleImplementation(decimal grossValue)
        {
            decimal accumulatedTax = 0;
            // loop through each handler, accumulate total tax
            foreach (var handler in _handlers)
            {
                accumulatedTax += handler.Handle(grossValue);
            }
            return accumulatedTax;
        }
    }
}
