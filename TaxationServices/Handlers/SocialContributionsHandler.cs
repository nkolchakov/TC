using Microsoft.Extensions.Logging;
using TaxationServices.Interfaces;

namespace TaxationServices.Handlers
{
    public class SocialContributionsHandler : TaxHandler
    {
        private readonly ITaxationService _taxationService;
        private readonly ILogger _logger;
        public SocialContributionsHandler(ITaxationService taxationService,
            ILogger<SocialContributionsHandler> logger)
        {
            _taxationService = taxationService ?? throw new ArgumentNullException(nameof(taxationService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override decimal HandleImplementation(decimal grossValue)
        {
            var scTax = _taxationService.GetSocialContributions(grossValue);

            _logger.LogInformation(string.Format(TaxMessages.SOCIAL_CONTRIBUTION_LOG, scTax));
            return scTax;
        }
    }
}
