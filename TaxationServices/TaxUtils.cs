using TaxationServices.Interfaces;

namespace TaxationServices
{
    public class TaxUtils : ITaxUtils
    {
        public void ValidateHandlers(IEnumerable<TaxHandler> handlers)
        {
            if (!handlers.Any())
            {
                throw new ArgumentException(TaxMessages.NO_CHAINED_HANDLERS);
            }
            if (handlers.Any(h => h == null))
            {
                throw new ArgumentNullException(TaxMessages.CHAINED_HANDLER_NOT_REGISTERED);
            }
        }
    }
}
