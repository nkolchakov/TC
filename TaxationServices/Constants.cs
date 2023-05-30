namespace TaxationServices
{
    public static class Constants
    {
        /**
         * legend:
         * SC - Social Contributions
         */
        public const decimal INPUT_MIN_VALUE = 0;

        public const int INCOME_TAX_PERCENTAGE = 10;
        public const int SC_TAX_PERCENTAGE = 15;

        public const int MIN_TAXABLE_INCOME = 1000;
        public const int SC_MIN_TAXABLE_INCOME = 1000;
        public const int SC_MAX_TAXABLE_INCOME = 3000;
    }

    /// <summary>
    /// Extracted strings for i18n.
    /// </summary>
    public static class TaxMessages
    {
        // Client error messages
        public const string NON_PARSABLE_INPUT = "Input could not be parsed. Try again.";
        public const string INVALID_RANGE = "Invalid input range. Positive income is allowed.";
        public const string NO_CHAINED_HANDLERS = "No handlers are chained !";
        public const string CHAINED_HANDLER_NOT_REGISTERED = "Chained handler is requested but not registered !";

        public const string MESSAGE_INPUT = "\n\r=== Enter gross salary ({0}): ";
        public const string END_COMMAND = "end";
        public const string CURRENCY_SYMBOL = "IDR";
        public const string SEPARATOR_INFO = "Info: Use ',' or '.' depending on locale";

        // Logs
        public const string SOCIAL_CONTRIBUTION_LOG = "  -[Social contribution tax]: {0:C}.";
        public const string INCOME_TAX_LOG = "  -[Income tax]: {0:C}.";
        public const string NET_INCOME_LOG = "[NET income]: {0:C}.";
    }
}
