namespace TaxationServices.Interfaces
{
    /// <summary>
    /// Template Method Pattern. 
    /// Each Handler will implement it's own tax calculation logic (<see cref="HandleImplementation(decimal)"/>).
    /// I Decided to use this pattern because I didn't want to duplicate the validation logic (<see cref="ValidateIncome(decimal)"/>) 
    /// (and any other repeating pre/post processing) inside every handler. 
    /// This way they can be used safely in isolation. 
    /// Another approach is to check the validation before calling the (<see cref="Handlers.TaxationAggregator"/>) and trust the input down the chain.
    /// </summary>
    public abstract class TaxHandler
    {
        protected abstract decimal HandleImplementation(decimal grossValue);

        private static void ValidateIncome(decimal grossIncome)
        {
            if (grossIncome <= Constants.INPUT_MIN_VALUE)
            {
                throw new ArgumentOutOfRangeException(paramName: null, message: TaxMessages.INVALID_RANGE);
            }
        }

        public decimal Handle(decimal grossIncome)
        {
            ValidateIncome(grossIncome);

            decimal tax = HandleImplementation(grossIncome);
            return tax;
        }
    }
}
